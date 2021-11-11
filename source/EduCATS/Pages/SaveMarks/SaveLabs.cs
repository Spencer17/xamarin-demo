﻿using EduCATS.Networking.Models.SaveMarks;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCATS.Pages.SaveMarks
{
	public class SaveLabs
	{
		public List<int> Id = new List<int>();
		public List<string> comments = new List<string>();
		public int dateId { get; set; }
		public List<string> marks = new List<string>();
		public List<LaboratoryWorksModel> students { get; set; }
		public List<bool> showForStudents = new List<bool>();
		public SaveLabs()
		{
			students = new List<LaboratoryWorksModel>();
		}
		public List<int> studentsId = new List<int>();
	}
}
