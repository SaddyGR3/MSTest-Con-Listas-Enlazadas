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

public class ListaEnlazada : IList
{
    private Nodo Cabeza;
    private Nodo Cola;

    public ListaEnlazada()
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
        Console.WriteLine(); // Para imprimir una nueva línea después de la lista
    }

    public void InsertInOrder(int value) //Este metodo inserta un nodo en la lista enlazada de forma ascendente siempre,por condicion de la tarea.
    {
        Nodo nuevoNodo = new Nodo(value); //Crea un nuevo nodo con el valor dado en la entrada al llamar el metodo.
        if (Cabeza == null) //si la cabeza es nula,significa que la lista esta vacia,entonces la cabeza y la cola apuntan al nuevo nodo
        {
            Cabeza = nuevoNodo; //Tanto la cabeza como la cola ahora son el nuevo nodo,porque es el unico que hay.
            Cola = nuevoNodo;
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

        int valor = Cabeza.Dato; //Guarda el valor de la cabeza en una variable.
        Cabeza = Cabeza.Siguiente; //La cabeza ahora es el nodo siguiente al actual. 

        if (Cabeza != null) //Si hay almenos un nodo en la lista.
        {
            Cabeza.Anterior = null; //El nodo anterior de la cabeza es nulo. Osea desvincula la cabeza del nodo anterior ya que este ahora no existe.
        }
        else
        {
            Cola = null; // La lista quedó vacía
        }

        return valor;
    }

    public int DeleteLast()
    {
        if (Cola == null)
        {
            throw new InvalidOperationException("La lista está vacía.");
        }

        int valor = Cola.Dato;//Guarda el valor de la cola en una variable.
        Cola = Cola.Anterior; //La cola ahora es el nodo anterior al actual.

        if (Cola != null)
        {
            Cola.Siguiente = null;
        }
        else
        {
            Cabeza = null; // La lista quedó vacía
        }

        return valor;
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
        if (Cabeza == null)
        {
            throw new InvalidOperationException("La lista está vacía.");
        }

        Nodo lento = Cabeza;
        Nodo rapido = Cabeza;

        while (rapido != null && rapido.Siguiente != null)
        {
            lento = lento.Siguiente;
            rapido = rapido.Siguiente.Siguiente;
        }

        return lento.Dato;
    }

    public void MergeSorted(IList listA, IList listB, SortDirection direction)
    {
        if (listA == null || listB == null)
        {
            throw new ArgumentNullException("Una o ambas listas son nulas.");
        }

        ListaEnlazada listaA = (ListaEnlazada)listA;
        ListaEnlazada listaB = (ListaEnlazada)listB;

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

        // Aquí manejamos los dos casos por separado: Ascendente y Descendente
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

                    // Insertar currentB antes de currentA en listA
                    if (previousA == null)
                    {
                        listaA.Cabeza = currentB; // currentB se convierte en la nueva cabeza de listA
                    }
                    else
                    {
                        previousA.Siguiente = currentB; // Conectar el nodo anterior a currentB
                    }

                    currentB.Anterior = previousA;
                    currentB.Siguiente = currentA;

                    if (currentA != null)
                    {
                        currentA.Anterior = currentB;
                    }
                    else
                    {
                        listaA.Cola = currentB; // Si currentA es nulo, currentB se convierte en la nueva cola de listA
                    }

                    previousA = currentB;
                    currentB = nextB;
                }
            }
        }
        else if (direction == SortDirection.Descending)
        {
            // Fusión en orden descendente
            while (currentA != null && currentB != null)
            {
                if (currentA.Dato >= currentB.Dato) //Si el dato de A es mayor o igual al dato de B
                {
                    previousA = currentA; //Se guarda el nodo actual de A en previousA
                    currentA = currentA.Siguiente; //Se avanza al siguiente nodo de A
                }
                else
                {
                    Nodo nextB = currentB.Siguiente; //Si B es mayor que A, nextB guarda el siguiente nodo de B

                    // Insertar currentB antes de currentA en listA
                    if (previousA == null)
                    {
                        listaA.Cabeza = currentB; // currentB se convierte en la nueva cabeza de listA
                    }
                    else
                    {
                        previousA.Siguiente = currentB; //Conectar el nodo anterior de A a currentB
                    }

                    currentB.Anterior = previousA;
                    currentB.Siguiente = currentA; // conectar B antes de A en la lista enlazada

                    if (currentA != null)
                    {
                        currentA.Anterior = currentB;
                    }
                    else
                    {
                        listaA.Cola = currentB; //Si currentA es nulo, currentB se convierte en la nueva cola de listA
                    }

                    previousA = currentB;
                    currentB = nextB; // Mover currentB al siguiente nodo
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

            // Mover hasta el final de listB
            while (currentB.Siguiente != null)
            {
                currentB = currentB.Siguiente;
            }

            // Actualizar la cola de listA
            listaA.Cola = currentB;
        }
    }
}


public class Program
{
    static void Main(string[] args)
    {
        // Crear las listas
        ListaEnlazada listA = new ListaEnlazada();
        listA.InsertInOrder(10);
        listA.InsertInOrder(15);

        ListaEnlazada listB = new ListaEnlazada();
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
