#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <ctype.h>
#include <mysql.h>
#include <pthread.h>
#include <time.h>
//Personas
typedef struct{
	char nombre[20];	
} Persona;

typedef struct {
	Persona personas[100];
	int num;
} ListaPersonas;
char Conectados[200];
ListaPersonas lista;

//Cartas
typedef struct{
	int palo; //Corazones,picas,treboles o diamantes
	int valor; //Numero de la carta 10, 4, As... (2=2) (J=10) ect... 
}Carta;

typedef struct{
	Carta cartas[200];
	int num;
} ListaCartas;

void DameCartas(ListaPersonas *lista, ListaCartas *listaC )
{
	int i = 0; //Primero tenemos que ver cuantoa jugadores tenemos para posteriormente repartir cartas
	//las 5 primeras cartas son de la mesa siempr
	
	srand(time(NULL));
	
	//while (i < ListaPersonas->num)
/*	{*/
/*		listaC->cartas[i].valor = rand()%14; *///es 1 m�s del n�mero que queremos generar
/*		listaC->cartas[i].palo = rand()%5;*/
/*		if (listaC*/
/*		i++;*/
/*	}*/
}
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

//void DameCartas(

void AddConectado (ListaPersonas *lista, int x, char usuario[20])
{
	x = lista->num;
	strcpy (lista->personas[x].nombre, usuario);
	lista->num = lista->num + 1;			
	printf("Nuevo jugador conectado %s\n", lista->personas[x].nombre);
	printf("Numero de jugadores conectados : %d\n", lista->num);
}

void EliminarConectado (ListaPersonas *lista, char usuario[20])
{
	for (int i=0; i<lista->num; i++)
	{
		if (lista->personas[i].nombre == usuario)
			strcpy (lista->personas[i].nombre, lista->personas[i+1].nombre);			
		else
			strcpy (lista->personas[i].nombre, lista->personas[i].nombre);
	}	
	lista->num--;
}

void ListaConectados (ListaPersonas *lista, char Conectados[100], char nombre[20], int cant)
{
	printf("Inicio de la ListaConectados\n");
	if (lista->num == 1)
	{
		strcpy (Conectados, lista->personas[0].nombre);
	}
	else if (lista->num > 1)
	{
		strcpy  (Conectados, lista->personas[0].nombre);
		while(cant < lista->num)
		{
			sprintf (nombre,"/%s",lista->personas[cant].nombre);
			strcat (Conectados, nombre);
			cant++;
		}
	}
	else if (lista->num == 0)
	{
		strcpy (Conectados, "Ninguno");
	}
	
}


void *AtenderCliente (void *socket)
{
	int sock_conn;
	int *s;
	s= (int *) socket;
	sock_conn= *s;
	int terminar = 0;
	
	//Conexion a la base de datos
	MYSQL *conn;
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;	
	//Creamos una conexion al servidor MYSQL 
	conn = mysql_init(NULL);
	if (conn==NULL) {
		printf ("Error al crear la conexi??n: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	//inicializar la conexion
	conn = mysql_real_connect (conn, "localhost","root", "mysql", "Juego",0, NULL, 0);
	if (conn==NULL) {
		printf ("Error al inicializar la conexi??n: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	// consulta SQL para obtener una tabla con todos los datos
	// de la base de datos
	err=mysql_query (conn, "SELECT * FROM Jugador,Partida,Relacion");
	
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}	
	resultado = mysql_store_result (conn);
	//Finalizacion de la conexion con la base de datos
	printf("Conexion a la base de datos correcta\n");
	
	int sock_listen, ret;
	struct sockaddr_in serv_adr;
	char peticion[512];
	char respuesta[512];
	
	int i;
	while (terminar ==0)
	{
		// Ahora recibimos la petici?n
		ret=read(sock_conn,peticion, sizeof(peticion));
		printf ("Recibido\n");
		
		// Tenemos que a?adirle la marca de fin de string 
		// para que no escriba lo que hay despues en el buffer
		peticion[ret]='\0';
		
		
		printf ("Peticion: %s\n",peticion);
		
		// vamos a ver que quieren
		char *p = strtok( peticion, "/");
		int codigo =  atoi (p);
		// Ya tenemos el c?digo de la petici?n
		
		if (err!=0) {
			printf ("Error al consultar datos de la base %u %s\n",
					mysql_errno(conn), mysql_error(conn));
			exit (1);
		}	
		resultado = mysql_store_result (conn);
		//Finalizacion de la conexion con la base de datos
		printf("Conexion a la base de datos correcta\n");
		
		
		char usuario[20];
		char contra[20];
		char nombre[20];
		char consulta[20];
		int x;
		
		if ((codigo ==1) || (codigo ==2))
		{
			p = strtok( NULL, "/");				
			strcpy (usuario, p);
			p = strtok( NULL, "/");				
			strcpy (contra, p);				
			printf ("Codigo: %d, Usuario: %s, Contrase�a: %s\n", codigo, usuario, contra);
			AddConectado(&lista, x, usuario);
		}			
		else if ((codigo ==3) || (codigo ==4) || (codigo ==5) || (codigo ==6)|| (codigo ==7))
		{
			p = strtok( NULL, "/");				
			strcpy (consulta, p);
			printf ("Codigo: %d, Consulta: %s\n", codigo, consulta);
		}
		
		
		int totalj;
		char totaljug[2];
		char sql[100];
		if (codigo ==0)
		{
			terminar=1;
			EliminarConectado (&lista, usuario);
		}			
		else if (codigo ==1) 
		{
			strcpy (sql, "SELECT Jugador.Identificador FROM (Jugador,Partida,Relacion) WHERE Jugador.Nombre = '");
			strcat (sql, usuario); 
			strcat (sql, "' AND Jugador.Contrase�a = '");
			strcat (sql, contra); 
			strcat (sql, "'");
			mysql_query (conn, sql);
			resultado = mysql_store_result (conn); 
			row = mysql_fetch_row (resultado);
			if (row == NULL)
				sprintf(respuesta,"1/Incorrecto");
			else
			{
				sprintf(respuesta,"1/Correcto");
			}				
		}
		else if (codigo ==2)
		{
			strcpy (sql, "SELECT count(*) from Jugador");
			mysql_query (conn, sql);
			resultado = mysql_store_result (conn); 
			row = mysql_fetch_row (resultado);
			strcpy(respuesta, row[0]);
			totalj = atoi(respuesta);
			totalj = totalj + 1;
			sprintf (totaljug, "%d\n", totalj);
			strcpy (sql, "INSERT INTO Jugador VALUES(");
			strcat (sql, totaljug);
			strcat (sql, ",'");
			strcat (sql, usuario); 
			strcat (sql, "','");
			strcat (sql, contra); 
			strcat (sql, "')");
			mysql_query (conn, sql);
			strcpy (respuesta,"2/Correcto");
		}
		else if (codigo ==3)
		{			
			strcpy (sql, "SELECT Relacion.Puntos FROM (Jugador,Partida,Relacion) WHERE Jugador.Nombre = '");
			strcat (sql, consulta);
			strcat (sql, "' AND Relacion.idJugador = Jugador.Identificador");
			mysql_query (conn, sql);
			resultado = mysql_store_result (conn); 
			row = mysql_fetch_row (resultado);
			sprintf(respuesta,"3/%s",row[0]);			
		}
		else if (codigo ==4)
		{
			strcpy (sql, "SELECT Partida.Fecha FROM (Partida) WHERE Partida.Identificador = ");
			strcat (sql, consulta);
			mysql_query (conn, sql);
			resultado = mysql_store_result (conn); 
			row = mysql_fetch_row (resultado);
			sprintf(respuesta, "4/%s",row[0]);
		}
		else if (codigo ==5)
		{
			strcpy (sql, "SELECT Jugador.Nombre FROM (Jugador,Partida,Relacion) WHERE Relacion.Puntos = 3 AND Relacion.idJugador = Jugador.Identificador AND Relacion.idPartida = ");
			strcat (sql, consulta);
			strcat (sql, " AND Relacion.idPartida = Partida.Identificador;");
			mysql_query (conn, sql);
			resultado = mysql_store_result (conn); 
			row = mysql_fetch_row (resultado);
			sprintf(respuesta, "5/%s",row[0]);
		}
		else if (codigo ==6)
		{
			char name[20];
			char Conectados[100]; //Se guarda un string con todos los conectados
			int cant=1;
			ListaConectados (&lista, Conectados, name, cant);
			//sprintf(respuesta, "6/%s",Conectados);
			sprintf(respuesta,"6/%s", Conectados);
		}
		else if (codigo =7) //quiero invitar a alguien
			//Cabe recalcar que vendran varios nombres separados por /
			//por ejemplo 7/Pedro/Maria quiere decir que quiere invitar a pedro y a Maria
		{
			char nombre2[20];
			int encontrado = 0;
			char *p = strtok(Conectados, "/");
			strcpy(nombre2, p);
			if(strcmp(consulta, nombre2)==0)
			{
				printf("El jugador %s est� conectado",nombre);
			}
			
			while ((p != NULL) && (encontrado == 0));
			{ 
				p = strtok(Conectados, "/");
				strcpy(nombre2, p);
				if(strcmp(consulta, nombre2)==0)
				{
					printf("El jugador %s est� conectado",nombre);
					encontrado = 1;
				}
			}
			if (encontrado == 0)
			{
				printf("Jugador no encontrado");
			}
		}
		if (codigo !=0)
		{				
			printf ("Respuesta: %s\n", respuesta);
			// Enviamos respuesta
			write (sock_conn,respuesta, strlen(respuesta));
		}
	}
	// Se acabo el servicio para este cliente
	close(sock_conn);
	mysql_close (conn);
	
}





int main(int argc, char *argv[])
{
	int sock_conn, sock_listen, ret;
	struct sockaddr_in serv_adr;
	char peticion[512];
	char respuesta[512];
	int puerto = 9010;
	 //Iniciamos con todos los jugadores desconectados
	// INICIALITZACIONS
	// Obrim el socket
	lista.num =0;
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creant socket");
	// Fem el bind al port
	
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	
	// asocia el socket a cualquiera de las IP de la m?quina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	// establecemos el puerto de escucha
	serv_adr.sin_port = htons(puerto);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	
	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");
	
	
	int sockets[100];
	pthread_t thread;
	int i=0;
	
	for (;;){
		printf ("Escuchando\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		
		sockets[i] =sock_conn; //AQUI TENEDREMOS EL SOCKET DEL CLIENTE 
		//sock_conn es el socket que usaremos para este cliente
		
		// Crear thead y decirle lo que tiene que hacer
		
		pthread_create (&thread, NULL, AtenderCliente,&sockets[i]);
		i=i+1;
		
	}
	
}
