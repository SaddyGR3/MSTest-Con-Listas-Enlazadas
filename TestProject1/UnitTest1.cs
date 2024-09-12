namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        //Primera Prueba de la tabla listA null y listB con cualquier valor,debe tirar Exception
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ListaA_Nula_tira_excepcion()
        {
            // Arrange
            ListaDoble listA = null;
            ListaDoble listB = new ListaDoble();
            listB.InsertInOrder(1);

            // Act
            listB.MergeSorted(listA, listB, SortDirection.Ascending);
        }
        //Segunda Prueba de la tabla listA con cualquier valor y listB null,debe tirar Exception
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ListaB_Nula_tira_excepcion()
        {
            // Arrange
            ListaDoble listB = null;
            ListaDoble listA = new ListaDoble();
            listA.InsertInOrder(1);

            // Act
            listA.MergeSorted(listA, listB, SortDirection.Ascending);
        }
        //Tercera Prueba, listA y listB con varios valores pero misma cantidad ambos,debe unir ambas lista de forma ascendente.
        [TestMethod]
        public void Union_Ascendente()
        {
            // Arrange
            ListaDoble listA = new ListaDoble();
            listA.InsertInOrder(0);
            listA.InsertInOrder(2);
            listA.InsertInOrder(6);
            listA.InsertInOrder(10);
            listA.InsertInOrder(25);

            ListaDoble listB = new ListaDoble();
            listB.InsertInOrder(3);
            listB.InsertInOrder(7);
            listB.InsertInOrder(11);
            listB.InsertInOrder(40);
            listB.InsertInOrder(50);

            // Act
            listA.MergeSorted(listA, listB, SortDirection.Ascending);

            // Assert
            int[] expectedValues = { 0, 2, 3, 6, 7, 10, 11, 25, 40, 50 };
            AssertListValues(listA, expectedValues);
        }

        //4ta prueba, listaA y listaB con varios valores pero listaA posee menos valors que listaB,debe unir ambas lista de forma descentente.
        [TestMethod]
        public void Unir_Descendente()
        {
            // Arrange
            ListaDoble listA = new ListaDoble();
            listA.InsertInOrder(10);
            listA.InsertInOrder(15);

            ListaDoble listB = new ListaDoble();
            listB.InsertInOrder(9);
            listB.InsertInOrder(40);
            listB.InsertInOrder(50);

            // Act
            listA.MergeSorted(listA, listB, SortDirection.Descending);

            // Assert
            int[] expectedValues = { 50, 40, 15, 10, 9 };
            AssertListValues(listA, expectedValues);
        }
        //5ta Prueba, listaA vacia y listaB con varios valores,debe devolver listaB de forma descendente.
        [TestMethod]
        public void ListA_Vacia_Devuelve_ListaB_Descendente()
        {
            // Arrange
            ListaDoble listA = new ListaDoble(); //Vacia
            ListaDoble listB = new ListaDoble();
            listB.InsertInOrder(9);
            listB.InsertInOrder(40);
            listB.InsertInOrder(50);

            // Act
            listA.MergeSorted(listA, listB, SortDirection.Descending);

            // Assert
            int[] expectedValues = { 50, 40, 9 };
            AssertListValues(listA, expectedValues);
        }
        //6ta prueba, listaA con varios valores y listaB vacia,debe devolver listaA de forma ascendente.
        [TestMethod]
        public void ListB_Vacia_Devuelve_ListaA_Ascendente()
        {
            // Arrange
            ListaDoble listA = new ListaDoble();
            listA.InsertInOrder(10);
            listA.InsertInOrder(15);

            ListaDoble listB = new ListaDoble(); // Empty

            // Act
            listA.MergeSorted(listA, listB, SortDirection.Ascending);

            // Assert
            int[] expectedValues = { 10, 15 };
            AssertListValues(listA, expectedValues);
        }
        //Primera Prueba Segundo problema, lista null devuelve exception
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Invert_lista_nula()
        {

            // Arrange
            ListaDoble lista = null;

            // Act
            lista.Invert(lista);

            // Assert
            Assert.IsNull(lista);
        }
        //Segunda Prueba Segundo problema, lista vacia, devuelve que la lista esta vacia
        [TestMethod]
        public void Invert_lista_vacia()
        {
            // Arrange
            ListaDoble lista = new ListaDoble();

            // Act
            lista.Invert(lista);

            // Assert
            Assert.IsNull(lista.GetHead(), "La lista esta vacia");
        }
        //Tercera Prueba Segundo problema, lista con varios valores,debe devolver la lista invertida
        [TestMethod]
        public void Invert_lista_valores()
        {
            // Arrange
            ListaDoble lista = new ListaDoble();
            lista.Insertar(1);
            lista.Insertar(0);
            lista.Insertar(30);
            lista.Insertar(50);
            lista.Insertar(2);

            // Act
            lista.Invert(lista);

            // Assert
            int[] expectedValues = { 2,50,30,0,1 };
            AssertListValues(lista, expectedValues);
        }
        //4ta Prueba Segundo problema, lista con 1 solo valor,debe devolver el mismo valor
        [TestMethod]
        public void Invert_lista_valor()
        {
            // Arrange
            ListaDoble lista = new ListaDoble();
            lista.Insertar(2);


            // Act
            lista.Invert(lista);

            // Assert
            int[] expectedValues = {2};
            AssertListValues(lista, expectedValues);
        }
        //Primera Prueba Tercer problema, lista null devuelve exception
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ObtenerMedio_Null()
        {
            // Arrange
            ListaDoble lista = null;


            // Act
            lista.GetMiddle();

            // Assert
            Assert.IsNull(lista);
        }
        //Segunda Prueba Tercer problema, lista null devuelve exception
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ObtenerMedio_Vacia()
        {
            // Arrange
            ListaDoble lista = new ListaDoble();

            // Act
            lista.GetMiddle();

            // Assert
            Assert.IsNull(lista);
        }
        //Tercera Prueba Tercer problema, lista con 1 valor,devuelve solo el valor
        [TestMethod]
        public void ObtenerMedio_uno()
        {
            // Arrange
            ListaDoble lista = new ListaDoble();
            lista.InsertInOrder(1);

            // Act
            int valorMedio = lista.GetMiddle(); // Capturamos el valor del nodo medio

            // Assert
            Assert.AreEqual(1, valorMedio, "deberia ser 1");
        }
        //cuarta Prueba Tercer problema, lista con 1 valor,devuelve solo el valor
        [TestMethod]
        public void ObtenerMedio_duo()
        {
            // Arrange
            ListaDoble lista = new ListaDoble();
            lista.InsertInOrder(1);
            lista.InsertInOrder(2);

            // Act
            int valorMedio = lista.GetMiddle(); // Capturamos el valor del nodo medio

            // Assert
            Assert.AreEqual(2, valorMedio, "deberia ser 2");
        }
        //Quinta Prueba Tercer problema, lista con tres valores,devuelve el valor medio
        [TestMethod]
        public void ObtenerMedio_trio()
        {
            // Arrange
            ListaDoble lista = new ListaDoble();
            lista.InsertInOrder(0);
            lista.InsertInOrder(1);
            lista.InsertInOrder(2);

            // Act
            int valorMedio = lista.GetMiddle(); //Capturamos el valor del nodo medio

            // Assert
            Assert.AreEqual(1, valorMedio, "deberia ser 1");
        }
        //Sexta Prueba Tercer problema, lista con cuatro valores,devuelve el valor medio
        [TestMethod]
        public void ObtenerMedio_cuatro()
        {
            // Arrange
            ListaDoble lista = new ListaDoble();
            lista.InsertInOrder(0);
            lista.InsertInOrder(1);
            lista.InsertInOrder(2);
            lista.InsertInOrder(3);

            // Act
            int valorMedio = lista.GetMiddle(); // Capturamos el valor del nodo medio

            // Assert
            Assert.AreEqual(2, valorMedio, "deberia ser 2");
        }






        //Metodos para comparar los valores de las listas
        private void AssertListValues(ListaDoble list, int[] expectedValues)
        {
            Nodo current = list.GetHead();
            for (int i = 0; i < expectedValues.Length; i++)
            {
                Assert.IsNotNull(current, "A la lista le falto un nodo o varios");
                Assert.AreEqual(expectedValues[i], current.Dato, $"El valor en la posición {i} esta mal");
                current = current.Siguiente;
            }
            Assert.IsNull(current, "La lista tiene un nodo adicional al esperado");
        }
    }
}
