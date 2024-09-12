using System;
using System.Collections.Generic;
using System.Xml.Linq;


public enum SortDirection //Enumeración SortDirection, tecnicamente es una forma de marcar 2 estados posibles de forma comoda y rapida,
                          //Una enumeración es una forma conveniente y rápida de asignar nombres descriptivos a valores enteros
{
    Ascending,
    Descending
}

// Definición de la interfaz IList
public interface IList //Una interfaz establece un contrato que las clases deben cumplir, en este caso la interfaz IList establece los metodos que deben tener las clases que la implementen
{                       //Practicamente seria como el molde o las condiciones que deben cumplir las clases que la implementen.
    void InsertInOrder(int value);
    int DeleteFirst(); //no tiene entradada porque simplemente su objetivo es eliminar el primer elemento de la lista enlazada
    int DeleteLast(); //lo mismo aca solo que elimina el ultimo elemento de la lista enlazada
    bool DeleteValue(int value); //este busca el valor en la lista enlazada y si lo encuentra lo elimina y devuelve True,si no lo encuentra no hace nada y devuelve False
    int GetMiddle(); //este metodo devuelve el valor del nodo que se encuentra en la mitad de la lista enlazada, funciona con 2 punteros uno que avanza 1 nodo y otro que avanza 2 nodos a la vez
                       //Cuando el puntero 2 llega al final de la lista,significa que el puntero 1 esta en el medio. 
    void MergeSorted(IList listA, IList listB, SortDirection direction); //Este metodo recibe 2 listas enlazadas y las ordena de forma ascendente o descendente segun el parametro direction
                                                                         //ListA seria la primera lista y listB la segunda, el resultado de la lista ordenada se guarda en la lista A.
}

public class Nodo
{
    public int Dato { get; set; }
    public Nodo Siguiente { get; set; }
    public Nodo Anterior { get; set; }

    public Nodo(int dato)
    {
        Dato = dato;
        Siguiente = null;
        Anterior = null;
    }
}

public class ListaDoble : IList
{
    private Nodo Cabeza;
    private Nodo Cola;
    private int tamaño;
    private Nodo nodoMedio;

    public ListaDoble()
    {
        Cabeza = null;
        Cola = null;
    }
    public void ImprimirLista()
    {
        Nodo actual = Cabeza;
        while (actual != null)
        {
            Console.Write(actual.Dato + " ");
            actual = actual.Siguiente;
        }
        Console.WriteLine(); //Para imprimir una nueva línea después de la lista
    }
    private void ActualizarNodoMedio()
    {
        if (tamaño == 1) // Si solo hay un nodo, el medio es la cabeza
        {
            nodoMedio = Cabeza;
        }
        else if (tamaño > 1) // Si hay más de un nodo
        {
            Nodo actual = Cabeza;
            for (int i = 0; i < tamaño / 2; i++) // Recorre hasta llegar a la mitad
            {
                actual = actual.Siguiente;
            }
            nodoMedio = actual;
        }
    }

    public void InsertInOrder(int value) //Este metodo inserta un nodo en la lista enlazada de forma ascendente siempre,por condicion de la tarea.
    {   
        Nodo nuevoNodo = new Nodo(value); //Crea un nuevo nodo con el valor dado en la entrada al llamar el metodo.
        tamaño++; // Aumentamos el tamaño de la lista al insertar un nuevo nodo
        if (Cabeza == null) //si la cabeza es nula,significa que la lista esta vacia,entonces la cabeza y la cola apuntan al nuevo nodo
        {
            Cabeza = nuevoNodo; //Tanto la cabeza como la cola ahora son el nuevo nodo,porque es el unico que hay.
            Cola = nuevoNodo;
            nodoMedio = Cabeza;
        }

        else //si no es nulo significa que ya hay un nodo en la lista.
        {
            if (value < Cabeza.Dato) //si el valor es menor al de la cabeza entonces este nuevo nodo se convierte en la cabeza debido a que debe ir antes del mayor para cumplir ascendencia.
            {
                nuevoNodo.Siguiente = Cabeza;
                Cabeza.Anterior = nuevoNodo;
                Cabeza = nuevoNodo;
            }
            else if (value > Cola.Dato) //Pero si es mayor entonces se convierte en la cola porque debe ir despues del menor. Para cumplir que es ascendente.
            {
                nuevoNodo.Anterior = Cola;
                Cola.Siguiente = nuevoNodo;
                Cola = nuevoNodo;
            }
            else //Pero si es igual entonces se inserta en medio de la lista.
            {
                Nodo actual = Cabeza;
                while (actual.Siguiente != null && actual.Siguiente.Dato < value) //se maneja un bucle donde se recorre la lista hasta encontrar el nodo que sea mayor al valor dado.
                {                                           //el bucle se ejecuta mientras el nodo actual no sea nulo y el nodo siguiente sea menor al valor dado.
                    actual = actual.Siguiente;
                }
                nuevoNodo.Siguiente = actual.Siguiente;//aca se inserta el nuevo nodo en medio de la lista.
                nuevoNodo.Anterior = actual;//se establece el nodo anterior del nuevo nodo como el nodo actual.
                if (actual.Siguiente != null)
                {
                    actual.Siguiente.Anterior = nuevoNodo;
                }
                actual.Siguiente = nuevoNodo;
            }
        }
        ActualizarNodoMedio(); // Actualizamos la referencia al nodo medio
    }
    public Nodo GetHead() //Este metodo devuelve la cabeza de la lista enlazada.
    {
        return Cabeza;
    }

    public int DeleteFirst()
    {
        if (Cabeza == null)
        {
            throw new InvalidOperationException("La lista está vacía.");
        }

        int valor = Cabeza.Dato;
        Cabeza = Cabeza.Siguiente;
        if (Cabeza != null)
        {
            Cabeza.Anterior = null;
        }
        else
        {
            Cola = null;
        }

        tamaño--; // Reducimos el tamaño de la lista al eliminar el nodo
        ActualizarNodoMedio(); // Actualizamos la referencia al nodo medio

        return valor;
    }

    public int DeleteLast()
    {
        if (Cola == null)
        {
            throw new InvalidOperationException("La lista está vacía.");
        }

        int valor = Cola.Dato;
        Cola = Cola.Anterior;
        if (Cola != null)
        {
            Cola.Siguiente = null;
        }
        else
        {
            Cabeza = null;
        }

        tamaño--; // Reducimos el tamaño de la lista al eliminar el nodo
        ActualizarNodoMedio(); // Actualizamos la referencia al nodo medio

        return valor;
    }

    public void Insertar(int valor)
    {
        Nodo nuevoNodo = new Nodo(valor);
        if (Cabeza == null)
        {
            Cabeza = nuevoNodo;
            Cola = nuevoNodo;
        }
        else
        {
            Cola.Siguiente = nuevoNodo;
            nuevoNodo.Anterior = Cola;
            Cola = nuevoNodo;
        }
    }
    public void Invert(ListaDoble list)
    {
        if (list.Cabeza == null)
        {
            Console.WriteLine("La lista esta vacía");
            return; //No hay nada que invertir
        }

        Nodo current = list.Cabeza;
        Nodo prev = null;
        Nodo next = null;

        while (current != null)
        {
            next = current.Siguiente;
            current.Siguiente = prev;
            current.Anterior = next;
            prev = current;
            current = next;
        }

        list.Cabeza = prev;
    }
    public bool DeleteValue(int value)
    {
        if (Cabeza == null)
        {
            return false;
        }

        if (Cabeza.Dato == value)
        {
            DeleteFirst();
            return true;
        }

        if (Cola.Dato == value)
        {
            DeleteLast();
            return true;
        }

        Nodo actual = Cabeza; //Se crea un nodo actual que empieza en la cabeza.
        while (actual != null) //Mientras el nodo actual no sea nulo.
        {
            if (actual.Dato == value) //si se llega al dato que se busca eliminar 
            {
                actual.Anterior.Siguiente = actual.Siguiente; //esto significa que el nodo anterior al nodo actual ahora apunta al nodo siguiente.
                if (actual.Siguiente != null)
                {
                    actual.Siguiente.Anterior = actual.Anterior;
                }
                return true;
            }
            actual = actual.Siguiente;
        }
        return false; // No se encontró el valor
    }

    public int GetMiddle()
    {
        if (nodoMedio == null)
        {
            throw new InvalidOperationException("La lista está vacía.");
        }

        return nodoMedio.Dato;
    }


    public void MergeSorted(IList listA, IList listB, SortDirection direction)
    {
        if (listA == null || listB == null)
        {
            throw new ArgumentNullException("Una o ambas listas son nulas.");
        }

        ListaDoble listaA = (ListaDoble)listA;
        ListaDoble listaB = (ListaDoble)listB;

        // Si listA está vacía, directamente asignamos los nodos de listB a listA
        if (listaA.Cabeza == null)
        {
            if (direction == SortDirection.Ascending)
            {
                listaA.Cabeza = listaB.Cabeza;
                listaA.Cola = listaB.Cola;
            }
            else if (direction == SortDirection.Descending)
            {
                // Invertimos la lista B y asignamos a listA
                Nodo current = listaB.Cabeza;
                Nodo prev = null;
                Nodo next = null;

                while (current != null)
                {
                    next = current.Siguiente;
                    current.Siguiente = prev;
                    current.Anterior = next;
                    prev = current;
                    current = next;
                }

                listaA.Cabeza = prev; //El último nodo procesado es la nueva cabeza
                listaA.Cola = listaB.Cabeza; //El primer nodo original es la nueva cola
            }

            return;
        }

        Nodo InvertirLista(Nodo cabeza)
        {
            Nodo prev = null;
            Nodo current = cabeza;
            Nodo next = null;

            while (current != null)
            {
                next = current.Siguiente;
                current.Siguiente = prev;
                current.Anterior = next;
                prev = current;
                current = next;
            }

            return prev; // Devuelve la nueva cabeza (el último nodo original)
        }

        // Invertir las listas si es orden descendente
        if (direction == SortDirection.Descending)
        {
            listaA.Cabeza = InvertirLista(listaA.Cabeza);
            listaB.Cabeza = InvertirLista(listaB.Cabeza);
        }

        Nodo currentA = listaA.Cabeza;
        Nodo currentB = listaB.Cabeza;
        Nodo previousA = null;

        if (direction == SortDirection.Ascending)
        {
            // Fusión en orden ascendente
            while (currentA != null && currentB != null)
            {
                if (currentA.Dato <= currentB.Dato)
                {
                    previousA = currentA;
                    currentA = currentA.Siguiente;
                }
                else
                {
                    Nodo nextB = currentB.Siguiente;

                    if (previousA == null)
                    {
                        listaA.Cabeza = currentB;
                    }
                    else
                    {
                        previousA.Siguiente = currentB;
                    }

                    currentB.Anterior = previousA;
                    currentB.Siguiente = currentA;

                    if (currentA != null)
                    {
                        currentA.Anterior = currentB;
                    }
                    else
                    {
                        listaA.Cola = currentB;
                    }

                    previousA = currentB;
                    currentB = nextB;
                }
            }
        }
        else if (direction == SortDirection.Descending)
        {
            // Fusión en orden descendente (ahora que las listas están invertidas)
            while (currentA != null && currentB != null)
            {
                if (currentA.Dato >= currentB.Dato)
                {
                    previousA = currentA;
                    currentA = currentA.Siguiente;
                }
                else
                {
                    Nodo nextB = currentB.Siguiente;

                    if (previousA == null)
                    {
                        listaA.Cabeza = currentB;
                    }
                    else
                    {
                        previousA.Siguiente = currentB;
                    }

                    currentB.Anterior = previousA;
                    currentB.Siguiente = currentA;

                    if (currentA != null)
                    {
                        currentA.Anterior = currentB;
                    }
                    else
                    {
                        listaA.Cola = currentB;
                    }

                    previousA = currentB;
                    currentB = nextB;
                }
            }
        }

        // Si queda algún nodo en la lista B, añadirlos al final de la lista A
        if (currentB != null)
        {
            if (previousA == null)
            {
                listaA.Cabeza = currentB;
            }
            else
            {
                previousA.Siguiente = currentB;
            }

            currentB.Anterior = previousA;

            while (currentB.Siguiente != null)
            {
                currentB = currentB.Siguiente;
            }

            listaA.Cola = currentB;
        }

    }

    
}



public class Program
{
    static void Main(string[] args)
    {
        // Crear las listas
        ListaDoble listA = new ListaDoble();
        listA.InsertInOrder(10);
        listA.InsertInOrder(15);

        ListaDoble listB = new ListaDoble();
        listB.InsertInOrder(9);
        listB.InsertInOrder(40);
        listB.InsertInOrder(50);

        // Realizar la fusión en orden descendente
        listA.MergeSorted(listA, listB, SortDirection.Descending);

        // Imprimir el resultado
        Console.WriteLine("Lista fusionada en orden descendente:");
        listA.ImprimirLista();

        // Esperar a que el usuario presione una tecla antes de salir
        Console.ReadKey();
    }
}
