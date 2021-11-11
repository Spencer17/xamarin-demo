﻿using Newtonsoft.Json;

namespace EduCATS.Data.Models
{
	/// <summary>
	/// News subject details model.
	/// </summary>
	public class NewsSubjectModel
	{
		/// <summary>
		/// Subject name.
		/// </summary>
		[JsonProperty("Name")]
		public string Name { get; set; }
	}
}
