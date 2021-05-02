using System.Drawing;
using System.Collections.Generic;

namespace CircuitReverse
{
	// Class for lines
	class NetLine
	{
		public Color LineColor = Color.Red;
		public string NetName = "";

		// image coordinates for top and bottom layers
		public List<Point>[] LinePoints = {new List<Point>(), new List<Point>()};
	}

	// Project class
	class Project
	{
		public string ProjectFilePath = "";

		public List<NetLine> Lines = new List<NetLine>();
	}
}
