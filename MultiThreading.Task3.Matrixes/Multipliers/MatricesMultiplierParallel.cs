using MultiThreading.Task3.MatrixMultiplier.Matrices;
using System.Threading.Tasks;

namespace MultiThreading.Task3.MatrixMultiplier.Multipliers
{
    public class MatricesMultiplierParallel : IMatricesMultiplier
    {
        public IMatrix Multiply(IMatrix m1, IMatrix m2)
        {
			var newMatrix = new Matrix(m1.RowCount, m2.ColCount);

			Parallel.For(0, m1.RowCount, x =>
			{
				for (var y = 0; y < m2.ColCount; y++)
				{
					long cellValue = 0;

					for (var k = 0; k < m1.ColCount; k++)
					{
						cellValue += m1.GetElement(x, k) * m2.GetElement(k, y);
					}

					newMatrix.SetElement(x, y, cellValue);
				}
			});

			return newMatrix;
		}
    }    
}
