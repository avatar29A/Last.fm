// RecommendedArtists.cs created with MonoDevelop
// User: amr at 2:56 AM 12/7/2008
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
	
	
	public class RecommendedArtists : Pages<Artist>
	{
		public AuthenticatedUser User {get; private set;}
		
		public RecommendedArtists(AuthenticatedUser user, Session session)
			:base("user.getRecommendedArtists", session)
		{
			this.User = user;
		}
		
		internal override RequestParameters getParams ()
		{
			return User.getParams();
		}

		/// <summary>
		/// Returns a page of artists.
		/// </summary>
		/// <param name="page">
		/// A <see cref="System.Int32"/>
		/// </param>
		/// <returns>
		/// A <see cref="Artist"/>
		/// </returns>
		public override Artist[] GetPage (int page)
		{
			if(page < 1)
				throw new InvalidPageException(page, 1);
			
			RequestParameters p = getParams();
			p["page"] = page.ToString();
			
			XmlDocument doc = request("user.getRecommendedArtists", p);
			
			List<Artist> list = new List<Artist>();
			foreach(XmlNode node in doc.GetElementsByTagName("artist"))
				list.Add(new Artist(extract(node, "name"), Session));
			
			return list.ToArray();
		}
	}
}
