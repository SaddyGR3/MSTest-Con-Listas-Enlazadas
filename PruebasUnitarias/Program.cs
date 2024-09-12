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
    public void ImprimirLista() //Metodo para imprimir la lista debido a que hay que recorrer la lista para ir imprimiendo cada dato en ella.
    {
        Nodo actual = Cabeza;
        while (actual != null)
        {
            Console.Write(actual.Dato + " ");
            actual = actual.Siguiente;
        }
        Console.WriteLine(); 
    }
    private void ActualizarNodoMedio() //Metodo para ir actualizando el nodo del medio cada vez que se inserta o elimina un nodo de la lista.
    {
        if (tamaño == 1) //Si solo hay un nodo, el medio es la cabeza
        {
            nodoMedio = Cabeza;
        }
        else if (tamaño > 1) //Si hay mas de un nodo
        {
            Nodo actual = Cabeza;
            for (int i = 0; i < tamaño / 2; i++) //Recorre hasta llegar a la mitad
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
        ActualizarNodoMedio(); //Actualizamos la referencia al nodo medio
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

        int valor = Cabeza.Dato; //Se guarda el valor de la cabeza en una variable
        Cabeza = Cabeza.Siguiente; //Cabeza pasa a ser el nodo siguiente
        if (Cabeza != null)
        {
            Cabeza.Anterior = null; //Eliminamos la referencia al nodo anterior
        }
        else
        {
            Cola = null;
        }

        tamaño--; //Se reduce el tamaño de la lista al eliminar un nodo
        ActualizarNodoMedio(); //Se actualiza la referencia

        return valor;
    }

    public int DeleteLast()
    {
        if (Cola == null)
        {
            throw new InvalidOperationException("La lista está vacía.");
        }

        int valor = Cola.Dato; //Guarda el valor de la cola en una variable
        Cola = Cola.Anterior; //Cola pasa a ser el nodo anterior
        if (Cola != null)
        {
            Cola.Siguiente = null;//Se elimina la referencia al nodo siguiente
        }
        else
        {
            Cabeza = null;
        }

        tamaño--; //Se reduce el tamaño de la lista al eliminar un nodo
        ActualizarNodoMedio(); //Se actualiza la referencia

        return valor;
    }

    public void Insertar(int valor) //Metodo para insertar cualquier tipo de lista,sin orden.
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
    public void Invert(ListaDoble list)//Metodo para invertir la lista enlazada sin crear una nueva lista.
    {
        if (list.Cabeza == null)
        {
            Console.WriteLine("La lista esta vacía");
            return; //No hay nada que invertir
        }

        Nodo current = list.Cabeza; //La estrategia es utilizar 3 punteros, uno que apunta al nodo actual,otro al nodo anterior y otro al nodo siguiente.
        Nodo prev = null;
        Nodo next = null;

        while (current != null) //Cuando el nodo actual sea nulo significa que se llego al final de la lista que queriamos rotar
        {
            next = current.Siguiente;//Se guarda el nodo siguiente en una variable
            current.Siguiente = prev; //El nodo siguiente ahora apunta al nodo anterior, en un inicio es null.
            current.Anterior = next; //El nodo anterior ahora apunta al nodo siguiente, que es el nodo siguiente que guardamos.
            prev = current; //El nodo anterior ahora es el nodo actual
            current = next;//El nodo actual ahora es el nodo siguiente
        }

        list.Cabeza = prev; //El ultimo nodo procesado es la nueva cabeza
    }
    public bool DeleteValue(int value)//Eliminar el valor dado como argumento de la lista enlazada
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
                actual.Anterior.Siguiente = actual.Siguiente; //esto significa que el nodo anterior al nodo actual ahora apunta al nodo siguiente del nodo actual.
                if (actual.Siguiente != null) //pero si el nodo siguiente del nodo actual no es nulo
                {
                    actual.Siguiente.Anterior = actual.Anterior; //entonces el nodo anterior del nodo siguiente del nodo actual ahora apunta al nodo anterior del nodo actual.
                }
                return true;//Se borro exitosamente el nodo
            }
            actual = actual.Siguiente;//Si el nodo actual no es el que se busca,se pasa al siguiente nodo.
        }
        return false; // No se encontró el valor
    }

    public int GetMiddle()//Retorna el dato del medio unicamente accediendo una vez a memoria.
    {
        if (nodoMedio == null)
        {
            throw new InvalidOperationException("La lista está vacía.");
        }

        return nodoMedio.Dato;
    }


    public void MergeSorted(IList listA, IList listB, SortDirection direction)
    {
        //Excepcion si alguna de las listas es nula
        if (listA == null || listB == null)
        {
            throw new ArgumentNullException("Una o ambas listas son nulas.");
        }
       
        ListaDoble listaA = (ListaDoble)listA;
        ListaDoble listaB = (ListaDoble)listB;

        //Si listA esta vacía, se asigna listB a listA
        if (listaA.Cabeza == null)
        {
            if (direction == SortDirection.Ascending)
            {
                listaA.Cabeza = listaB.Cabeza; //Al pasar la cabeza y la cola de la lista B a la lista A, la lista A ahora es la lista B
                listaA.Cola = listaB.Cola;
            }
            else if (direction == SortDirection.Descending) //Si hay que ordenarla de forma descendente
            {
                //Invertimos la lista B y asignamos a listA
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

        Nodo InvertirLista(Nodo cabeza) //Otro metodo invertir lista,pero esta vez se recibe la cabeza de la lista a invertir.
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

        //Invertir las listas en orden descendente
        if (direction == SortDirection.Descending)
        {
            listaA.Cabeza = InvertirLista(listaA.Cabeza);
            listaB.Cabeza = InvertirLista(listaB.Cabeza);
        }

        Nodo currentA = listaA.Cabeza; //Los 3 punteros para recorrer las listas
        Nodo currentB = listaB.Cabeza;
        Nodo previousA = null;

        if (direction == SortDirection.Ascending)//ordenar ambas listas de forma ascendente
        {
            while (currentA != null && currentB != null)
            {
                if (currentA.Dato <= currentB.Dato)//Si el dato de la lista A es menor o igual al dato de la lista B
                {
                    previousA = currentA; //El nodo anterior de la lista A es el nodo actual de la lista A
                    currentA = currentA.Siguiente;//El nodo actual de la lista A ahora es el nodo siguiente de la lista A
                }
                else
                {
                    Nodo nextB = currentB.Siguiente; //Guardamos el nodo siguiente de la lista B

                    if (previousA == null) //Si el nodo anterior de la lista A es nulo
                    {
                        listaA.Cabeza = currentB; //Solo hay un nodo en la lista A,entonces la cabeza de la lista A ahora es el nodo actual de la lista B
                    }
                    else
                    {
                        previousA.Siguiente = currentB;//pero si no es nulo,entonces el nodo siguiente del nodo anterior de la lista A ahora es el nodo actual de la lista B
                    }

                    currentB.Anterior = previousA; //El nodo anterior del nodo actual de la lista B ahora es el nodo anterior de la lista A
                    currentB.Siguiente = currentA; //El nodo siguiente del nodo actual de la lista B ahora es el nodo actual de la lista A

                    if (currentA != null) //si el nodo actual de la lista A no es nulo
                    {
                        currentA.Anterior = currentB; //El nodo anterior del nodo actual de la lista A ahora es el nodo actual de la lista B
                    }
                    else
                    {
                        listaA.Cola = currentB; //Si el nodo actual de la lista A es nulo,entonces la cola de la lista A ahora es el nodo actual de la lista B
                    }

                    previousA = currentB; //se guarda el nodo actual de la lista B en el nodo anterior de la lista A
                    currentB = nextB; //se guarda el nodo siguiente de la lista B en el nodo actual de la lista B
                }
            }
        }
        else if (direction == SortDirection.Descending) //ordenar ambas listas de forma descendente,primero se invirtieron para facilitar el proceso.
        {
            while (currentA != null && currentB != null)
            {
                if (currentA.Dato >= currentB.Dato)  //Se sigue exactamente el mismo proceso que ascendente,solo que se cambia el signo de la comparacion. Y desde un inicio se rotaron para que quedaran de forma descendente desde un principio.
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

        //Si queda algun nodo en la lista B,se agrega al final de la lista A
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
        //Creacion de las listas
        ListaDoble listA = new ListaDoble();
        listA.InsertInOrder(10);
        listA.InsertInOrder(15);

        ListaDoble listB = new ListaDoble();
        listB.InsertInOrder(9);
        listB.InsertInOrder(40);
        listB.InsertInOrder(50);

        //Se unen ambas en descendente
        listA.MergeSorted(listA, listB, SortDirection.Descending);

        //Se imprime el resultado
        Console.WriteLine("Lista fusionada en orden descendente:");
        listA.ImprimirLista();

        //Cuando el usuario presiona una tecla, el programa se cierra.
        Console.ReadKey();
    }
}
