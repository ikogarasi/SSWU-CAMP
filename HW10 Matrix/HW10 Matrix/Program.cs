using HW10_Matrix;

Matrix matrix = new Matrix(4, 4);
matrix.GenerateDiagonalSnake();
Console.WriteLine(matrix);

foreach (int i in matrix.GetDiagonalMatrix())
{
    Console.Write(i + " ");
}

Console.WriteLine();
matrix.GenerateHorizontalSnake();
Console.WriteLine(matrix);

foreach (int i in matrix.GetHorizontalMatrix())
{
    Console.Write(i + " ");
}
