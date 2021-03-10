using System;
using System.Windows.Forms;

namespace CircuitReverse
{
	public class BufferedPanel : Panel
	{
		public BufferedPanel()
		{
			DoubleBuffered = true;
			ResizeRedraw = true;
		}
	}
}
