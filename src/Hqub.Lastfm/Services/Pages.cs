// Pages.cs created with MonoDevelop
// User: amr at 2:20 AM 12/7/2008
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
	/// An abstract class inherited by data objects that come in pages.
	/// </summary>
	public abstract class Pages<T> : Base
	{
		protected internal string methodName {get; set;}
		
		public Pages(string methodName, Session session)
			:base(session)
		{
			this.methodName = methodName;
		}
		
		public int GetPageCount()
		{
			XmlDocument doc = request(methodName);
			
			return int.Parse(doc.DocumentElement.ChildNodes[0].Attributes.GetNamedItem("totalPages").InnerText);
		}
		
		public int GetItemsPerPage()
		{
			XmlDocument doc = request(methodName);
			
			return int.Parse(doc.DocumentElement.ChildNodes[0].Attributes.GetNamedItem("perPage").InnerText);
		}
		
		public abstract T[] GetPage(int page);
	}
}
