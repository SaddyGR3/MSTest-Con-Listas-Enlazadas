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
            ListaEnlazada listA = null;
            ListaEnlazada listB = new ListaEnlazada();
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
            ListaEnlazada listB = null;
            ListaEnlazada listA = new ListaEnlazada();
            listA.InsertInOrder(1);

            // Act
            listA.MergeSorted(listA, listB, SortDirection.Ascending);
        }
        //Tercera Prueba, listA y listB con varios valores pero misma cantidad ambos,debe unir ambas lista de forma ascendente.
        [TestMethod]
        public void Union_Ascendente()
        {
            // Arrange
            ListaEnlazada listA = new ListaEnlazada();
            listA.InsertInOrder(0);
            listA.InsertInOrder(2);
            listA.InsertInOrder(6);
            listA.InsertInOrder(10);
            listA.InsertInOrder(25);

            ListaEnlazada listB = new ListaEnlazada();
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
            ListaEnlazada listA = new ListaEnlazada();
            listA.InsertInOrder(10);
            listA.InsertInOrder(15);

            ListaEnlazada listB = new ListaEnlazada();
            listB.InsertInOrder(9);
            listB.InsertInOrder(40);
            listB.InsertInOrder(50);

            // Act
            listA.MergeSorted(listA, listB, SortDirection.Descending);

            // Assert
            int[] expectedValues = {50, 40,15, 10, 9};
            AssertListValues(listA, expectedValues);
        }
        //5ta Prueba, listaA vacia y listaB con varios valores,debe devolver listaB de forma descendente.
        [TestMethod]
        public void ListA_Vacia_Devuelve_ListaB_Descendente()
        {
            // Arrange
            ListaEnlazada listA = new ListaEnlazada(); //Vacia
            ListaEnlazada listB = new ListaEnlazada();
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
            ListaEnlazada listA = new ListaEnlazada();
            listA.InsertInOrder(10);
            listA.InsertInOrder(15);

            ListaEnlazada listB = new ListaEnlazada(); // Empty

            // Act
            listA.MergeSorted(listA, listB, SortDirection.Ascending);

            // Assert
            int[] expectedValues = { 10, 15 };
            AssertListValues(listA, expectedValues);
        }

        // Helper method to compare list values
        private void AssertListValues(ListaEnlazada list, int[] expectedValues)
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
