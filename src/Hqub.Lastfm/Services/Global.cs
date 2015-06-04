// Global.cs
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
	/// Global functions that don't fit anywhere else.
	/// </summary>
	public class Global : Base
	{	
		public Global(Session session)
			:base(session)
		{
		}
		
		internal override RequestParameters getParams ()
		{
			return new Lastfm.RequestParameters();
		}

		
		/// <summary>
		/// Returns the most popular tags on Last.fm.
		/// </summary>
		/// <returns>
		/// A <see cref="TopTag"/>
		/// </returns>
		public TopTag[] GetTopTags()
		{
			XmlDocument doc = request("tag.getTopTags");
			
			List<TopTag> list = new List<TopTag>();
			foreach(XmlNode node in doc.GetElementsByTagName("tag"))
				list.Add(new TopTag(new Tag(extract(node, "name"), Session), Int32.Parse(extract(node, "count"))));
			
			return list.ToArray();
		}
	}
}
