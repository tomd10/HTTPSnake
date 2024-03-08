using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokus3
{
    public static class Graphics
    {
        public static void DrawGrid(ICanvas canvas)
        {
            float res = DataStore.res;
            float dim = DataStore.cfgData.dim;
            float spacing = (float)res / (float)dim;
            canvas.StrokeColor = Colors.DarkGray;
            canvas.StrokeSize = (float)res / 100;

            for (int i = 0; i <= dim; i++)
            {
                canvas.DrawLine(i * spacing, 0, i * spacing, res);
            }

            for (int i = 0; i <= dim; i++)
            {
                canvas.DrawLine(0, i * spacing, res, i * spacing);
            }
        }

        public static void DrawSnake(ICanvas canvas)
        {
            float res = DataStore.res;
            float dim = DataStore.cfgData.dim;
            float spacing = (float)res / (float)dim;
            if (DataStore.configured)
            {
                for (int i = 0; i < dim; i++)
                {
                    for (int j = 0; j < dim; j++)
                    {
                        Cell c = Game.GetCell(i, j);
                        if (c.state == Model.cellState.Head)
                        {
                            canvas.StrokeColor = Colors.Red;
                            canvas.FillColor = Colors.Red;
                            canvas.FillRectangle(i * spacing, j * spacing, spacing, spacing);
                        }
                        else if (c.state == Model.cellState.Body)
                        {
                            canvas.StrokeColor = Colors.Green;
                            canvas.FillColor = Colors.Green;
                            canvas.FillRectangle(i * spacing, j * spacing, spacing, spacing);
                        }
                        else if (c.state == Model.cellState.Food)
                        {
                            canvas.StrokeColor = Colors.Orange;
                            canvas.FillColor = Colors.Orange;
                            canvas.FillRectangle(i * spacing, j * spacing, spacing, spacing);
                        }
                    }
                }
            }
        }
    }
}
