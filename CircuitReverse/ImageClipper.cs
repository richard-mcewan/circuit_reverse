using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CircuitReverse
{
	class ImageClipper
	{
		public static Image ClipImage(Image img, List<Point> plist)
		{
			// Sort points, arrange in arrays
			plist.Sort(ComparePoints);

			var xx = new double[4];
			var yy = new double[4];
			for (int i = 0; i < 4; i++)
			{
				xx[i] = plist[i].X;
				yy[i] = plist[i].Y;
			}

			// Determine width and height
			var width = xx.Max() - xx.Min();
			var height = yy.Max() - yy.Min();

			var x = new double[] { 0, 0, width, width };
			var y = new double[] { 0, height, 0, height };

			// Switch the two left points if needed
			if (yy[0] > yy[1])
			{
				y[0] = height;
				y[1] = 0;
			}

			// Switch the two right points if needed
			if (yy[2] > yy[3])
			{
				y[2] = height;
				y[3] = 0;
			}

			// Create matrices
			var A = new double[][]
			{
				new double[] { x[0], y[0], 0, 0, 1, 0, -x[0]*xx[0], -y[0]*xx[0] },
				new double[] { 0, 0, x[0], y[0], 0, 1, -x[0]*yy[0], -y[0]*yy[0] },
				new double[] { x[1], y[1], 0, 0, 1, 0, -x[1]*xx[1], -y[1]*xx[1] },
				new double[] { 0, 0, x[1], y[1], 0, 1, -x[1]*yy[1], -y[1]*yy[1] },
				new double[] { x[2], y[2], 0, 0, 1, 0, -x[2]*xx[2], -y[2]*xx[2] },
				new double[] { 0, 0, x[2], y[2], 0, 1, -x[2]*yy[2], -y[2]*yy[2] },
				new double[] { x[3], y[3], 0, 0, 1, 0, -x[3]*xx[3], -y[3]*xx[3] },
				new double[] { 0, 0, x[3], y[3], 0, 1, -x[3]*yy[3], -y[3]*yy[3] }
			};

			var B = new double[][] {
				new double[] {xx[0] },
				new double[] {yy[0] },
				new double[] {xx[1] },
				new double[] {yy[1] },
				new double[] {xx[2] },
				new double[] {yy[2] },
				new double[] {xx[3] },
				new double[] {yy[3] }
			};

			var T = MatrixInv.MatrixProduct(MatrixInv.MatrixInverse(A), B);

			// Create new image

			var res = new Bitmap(dtoi(width), dtoi(height));
			using (var src = new Bitmap(img))
			{
				for (int xi = 0; xi < width; xi++)
				{
					for (int yi = 0; yi < height; yi++)
					{
						// Transform coordinates
						var z = T[6][0] * xi + T[7][0] * yi + 1;
						var xt = dtoi((T[0][0] * xi + T[1][0] * yi + T[4][0]) / z);
						var yt = dtoi((T[2][0] * xi + T[3][0] * yi + T[5][0]) / z);

						// Set pixel
						res.SetPixel(xi, yi, src.GetPixel(xt, yt));
					}
				}
			}

			return res;
		}

		public static Image ScaleImage(Image img, float scale)
		{
			var hs = (float)img.Size.Height / img.Size.Width / scale;
			return new Bitmap(img, dtoi(img.Size.Width * hs), img.Size.Height);
		}

		public static int ComparePoints(Point p1, Point p2)
		{
			if (p1.X > p2.X)
			{
				return 1;
			}
			else if (p1.X < p2.X)
			{
				return -1;
			}
			else if (p1.Y > p2.Y)
			{
				return 1;
			}
			else if (p1.Y < p2.Y)
			{
				return -1;
			}
			else
			{
				return 0;
			}
		}

		public static int dtoi(double a)
		{
			return (int)Math.Floor(a);
		}

	}
}
