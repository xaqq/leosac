/*
    Copyright (C) 2014-2022 Leosac

    This file is part of Leosac.

    Leosac is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Leosac is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program. If not, see <http://www.gnu.org/licenses/>.
*/

#include "exception/ModelException.hpp"
#include "tools/Schedule.hpp"
#include "gtest/gtest.h"
#include <boost/locale.hpp>

using namespace Leosac;
using namespace Leosac::Tools;

namespace Leosac
{
namespace Test
{

TEST(TestScheduleValidator, name_length)
{
    Schedule s;
    ASSERT_THROW({ s.name("s"); }, ModelException);


    ASSERT_NO_THROW({
        s.name("long_enough");
        ScheduleValidator::validate(s);
    });

    ASSERT_THROW(
        { s.name("this_is_so_long_this_name_is_clearly_to_long_to_be_valid"); },
        ModelException);
}

TEST(TestGroupValidator, name_invalid_char)
{
    Schedule s;
    ASSERT_NO_THROW({ s.name("aaaa"); });
    ASSERT_THROW({ s.name("aaa$"); }, ModelException);
    ASSERT_THROW({ s.name("aaa!"); }, ModelException);
    ASSERT_THROW({ s.name("aaa^"); }, ModelException);
    ASSERT_THROW({ s.name("aaa^"); }, ModelException);
    ASSERT_THROW({ s.name("aaaä"); }, ModelException);
    ASSERT_THROW({ s.name("aaaé"); }, ModelException);
    ASSERT_THROW({ s.name("aaa aaa"); }, ModelException);
}

TEST(TestGroupValidator, name_invalid_char_exception_message)
{
    bool has_thrown = false;
    Schedule s;
    try
    {
        s.name("aaa$");
    }
    catch (const ModelException &e)
    {
        has_thrown = true;
        ASSERT_EQ(std::string("Usage of unauthorized character: $"),
                  e.errors().at(0).message);
    }
    ASSERT_TRUE(has_thrown);
}

TEST(TestGroupValidator, name_invalid_char_exception_message_invalid_utf8)
{
    bool has_thrown = false;
    Schedule s;
    try
    {
        s.name("aaaä");
    }
    catch (const ModelException &e)
    {
        has_thrown = true;
        ASSERT_EQ(std::string("Usage of non ascii character starting with byte 0xC3"),
                  e.errors().at(0).message);
    }
    ASSERT_TRUE(has_thrown);
}
} // namespace Test
} // namespace Leosac
