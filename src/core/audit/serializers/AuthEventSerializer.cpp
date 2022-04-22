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

#include "AuthEventSerializer.hpp"
#include "AuditSerializer.hpp"
#include "core/SecurityContext.hpp"
#include "core/audit/IAuthEvent.hpp"
#include "tools/log.hpp"

namespace Leosac
{
namespace Audit
{
namespace Serializer
{
json AuthEventJSON::serialize(const Audit::IAuthEvent &in, const SecurityContext &sc)
{
    auto serialized = AuditJSON::serialize(in, sc);
    // Now we override the type.
    ASSERT_LOG(serialized.at("type").is_string(),
               "Base audit serialization did something unexpected.");
    serialized["type"] = "audit-auth-event";

    serialized["relationships"]["credential"] = {
        {{"id", in.credential_id()}, {"raw", in.credential_raw()}}};

    serialized["relationships"]["access_point"] = {
        {{"id", in.access_point_id()}}};

    return serialized;
}
}
}
}
