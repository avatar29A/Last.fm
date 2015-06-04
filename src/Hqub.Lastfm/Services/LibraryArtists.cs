// LibraryArtists.cs created with MonoDevelop
// User: amr at 4:19 AM 12/7/2008
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
	/// The artists in a <see cref="Library"/>.
	/// </summary>
	public class LibraryArtists : Pages<LibraryArtist>
	{
		
		/// <summary>
		/// The library.
		/// </summary>
		public Library Library {get; private set;}
		
		public LibraryArtists(Library library, Session session)
			:base("library.getArtists", session)
		{
			this.Library = library; 
		}
		
		internal override RequestParameters getParams ()
		{
			return Library.getParams();
		}
		
		public override LibraryArtist[] GetPage (int page)
		{
			if(page < 1)
				throw new Exception("The first page is 1.");
			
			RequestParameters p = getParams();
			p["page"] = page.ToString();
			
			XmlDocument doc = request("library.getArtists", p);

			List<LibraryArtist> list = new List<LibraryArtist>();
			
			foreach(XmlNode node in doc.GetElementsByTagName("artist"))
			{
				int playcount = 0;
				try
				{ playcount = Int32.Parse(extract(node, "playcount")); }
				catch (FormatException)
				{}
				
				int tagcount = 0;
				try
				{ tagcount = Int32.Parse(extract(node, "tagcount")); }
				catch (FormatException)
				{}
				
				Artist artist = new Artist(extract(node, "name"), Session);
				list.Add(new LibraryArtist(artist, playcount, tagcount));
			}
			
			return list.ToArray();
		}

	}
}
