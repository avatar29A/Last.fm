// WeeklyChartTimeSpan.cs
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

namespace Lastfm.Services
{
	/// <summary>
	/// A time span of a weekly chart.
	/// </summary>
	/// <remarks>
	/// Usually are returned by <see cref="IHasWeeklyAlbumCharts.GetWeeklyChartTimeSpans"/>, 
	/// <see cref="IHasWeeklyArtistCharts.GetWeeklyChartTimeSpans"/>, or 
	/// <see cref="IHasWeeklyTrackCharts.GetWeeklyChartTimeSpans"/>.
	/// </remarks>
	public class WeeklyChartTimeSpan
	{
		/// <summary>
		/// The beginning date.
		/// </summary>
		public DateTime From {get; private set;}
		
		/// <summary>
		/// The end date.
		/// </summary>
		public DateTime To {get; private set;}
		
		public WeeklyChartTimeSpan(DateTime from, DateTime to)
		{
			From = from;
			To = to;
		}
	}
}
