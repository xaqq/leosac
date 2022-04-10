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

#pragma once

#include "spdlog/details/null_mutex.h"
#include "tools/db/db_fwd.hpp"
#include <mutex>
#include <spdlog/sinks/base_sink.h>

namespace Leosac
{
namespace Tools
{

/**
 * A non template class to allow impl
 */
class DatabaseLogSinkHelper
{
  public:
    DatabaseLogSinkHelper(DBPtr database);
    void do_log(const spdlog::details::log_msg &msg);

  private:
    DBPtr database_;
    std::string run_id_;
};

/**
 * A custom sink that write LogEntry object
 * to a SQLite database.
 */
template <typename Mutex>
class DatabaseLogSink : public spdlog::sinks::base_sink<Mutex>
{
  public:
    /**
     * Construct a SQLite backed log sink.
     * @param database A non null pointer to a ODB database object.
     */
    DatabaseLogSink(DBPtr database)
        : helper_(database)
    {
    }

    void sink_it_(const spdlog::details::log_msg &msg) override
    {
        helper_.do_log(msg);
    }

    void flush_() override
    {
        // Noop as writing to a SQL database is transactional.
    }

  private:
    DatabaseLogSinkHelper helper_;
};

using DatabaseLogSink_mt = DatabaseLogSink<::std::mutex>;
using DatabaseLogSink_st = DatabaseLogSink<::spdlog::details::null_mutex>;
} // namespace Tools
} // namespace Leosac