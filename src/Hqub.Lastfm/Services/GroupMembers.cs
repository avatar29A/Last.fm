// GroupMembers.cs created with MonoDevelop
// User: amr at 2:26 AM 12/7/2008
//
//  Copyright (C) 2008 Amr Hassan
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
//
//

using System;
using System.Xml;
using System.Collections.Generic;

namespace Lastfm.Services
{
	/// <summary>
	/// The members of a Last.fm group.
	/// </summary>
	public class GroupMembers : Pages<User>
	{
		/// <value>
		/// The concerned group.
		/// </value>
		public Group Group {get; private set;}
		
		public GroupMembers(Group group, Session session)
			:base("group.getMembers", session)
		{
			Group = group;
		}
		
		internal override RequestParameters getParams ()
		{
			return Group.getParams();
		}
		
		public override User[] GetPage(int page)
		{
			if(page < 1)
				throw new InvalidPageException(page, 1);
			
			RequestParameters p = getParams();
			p["page"] = page.ToString();
			
			XmlDocument doc = Group.request("group.getMembers", p);
			
			List<User> list = new List<User>();
			foreach(XmlNode node in doc.GetElementsByTagName("user"))
				list.Add(new User(extract(node, "name"), Session));
			
			return list.ToArray();
		}
	}
}
