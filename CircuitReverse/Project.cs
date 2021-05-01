using System.Drawing;
using System.Collections.Generic;

namespace CircuitReverse
{
	class NetLine
	{
		public Color LineColor = Color.Red;
		public string NetName = "";
		public List<Point>[] LinePoints = {new List<Point>(), new List<Point>()};
	}

	class Project
	{
		public string ProjectFilePath = "";

		public List<NetLine> Lines = new List<NetLine>();
	}
}
