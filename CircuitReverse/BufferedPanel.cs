using System;
using System.Windows.Forms;

namespace CircuitReverse
{
	class BufferedPanel : Panel
	{
		public BufferedPanel()
		{
			DoubleBuffered = true;
			ResizeRedraw = true;
		}
	}
}
