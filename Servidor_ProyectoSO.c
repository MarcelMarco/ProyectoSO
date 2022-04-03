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

typedef struct{
	char nombre[20];	
} Persona;

typedef struct {
	Persona personas[100];
	int num;
} ListaPersonas;

ListaPersonas *lista;

pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

void *AddConectado (ListaPersonas *lista, int x, char usuario[20])
{
	for (x=0;x++;lista->personas[x].nombre != NULL)
		lista->num = x;
	strcpy (lista->personas[x].nombre, usuario);
	lista->num = lista->num + 1;			
	printf("Nuevo jugador conectado %s\n", lista->personas[x].nombre);
	printf("Numero de jugadores conectados : %d\n", lista->num);
}

void ListaConectados (ListaPersonas *lista, char frase[100], char nombre[20], int cant, int x)
{
	printf("Inicio de la ListaConectados");
	if (lista->num = 1)
		strcpy  (frase, lista->personas[0].nombre);
	else
	{
		strcpy  (frase, lista->personas[0].nombre);
		while(cant < lista->num)
		{
			sprintf (nombre,"/%s",lista->personas[cant].nombre);
			strcat (frase, nombre);
			cant++;
		}
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
			printf ("Codigo: %d, Usuario: %s, Contraseña: %s\n", codigo, usuario, contra);
/*			for (x=0;x++;lista->personas[x].nombre != NULL)*/
/*				lista->num = x;*/
/*			strcpy (lista->personas[x].nombre, usuario);*/
/*			lista->num = lista->num + 1;			*/
/*			printf("Nuevo jugador conectado %s\n", lista->personas[x].nombre);*/
/*			printf("Numero de jugadores conectados : %d\n", lista->num);*/
			AddConectado(&lista, x, usuario);
		}			
		else if ((codigo ==3) || (codigo ==4) || (codigo ==5) || (codigo ==6))
		{
			p = strtok( NULL, "/");				
			strcpy (consulta, p);
			printf ("Codigo: %d, Consulta: %s\n", codigo, consulta);
		}
		
		
		int totalj;
		char totaljug[2];
		char sql[100];	
		char *name[20];
		char *frase[100];
		int cant=1;
		if (codigo ==0)
			terminar=1;
		else if (codigo ==1) 
		{
			strcpy (sql, "SELECT Jugador.Identificador FROM (Jugador,Partida,Relacion) WHERE Jugador.Nombre = '");
			strcat (sql, usuario); 
			strcat (sql, "' AND Jugador.Contraseña = '");
			strcat (sql, contra); 
			strcat (sql, "'");
			mysql_query (conn, sql);
			resultado = mysql_store_result (conn); 
			row = mysql_fetch_row (resultado);
			if (row == NULL)
				strcpy (respuesta,"Incorrecto");
			else
			{
				strcpy (respuesta,"Correcto");
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
			strcpy (respuesta,"Correcto");
		}
		else if (codigo ==3)
		{
			
			strcpy (sql, "SELECT Relacion.Puntos FROM (Jugador,Partida,Relacion) WHERE Jugador.Nombre = '");
			strcat (sql, consulta);
			strcat (sql, "' AND Relacion.idJugador = Jugador.Identificador");
			mysql_query (conn, sql);
			resultado = mysql_store_result (conn); 
			row = mysql_fetch_row (resultado);
			strcpy(respuesta, row[0]);			
		}
		else if (codigo ==4)
		{
			strcpy (sql, "SELECT Partida.Fecha FROM (Partida) WHERE Partida.Identificador = ");
			strcat (sql, consulta);
			mysql_query (conn, sql);
			resultado = mysql_store_result (conn); 
			row = mysql_fetch_row (resultado);
			strcpy(respuesta, row[0]);
		}
		else if (codigo ==5)
		{
			strcpy (sql, "SELECT Jugador.Nombre FROM (Jugador,Partida,Relacion) WHERE Relacion.Puntos = 3 AND Relacion.idJugador = Jugador.Identificador AND Relacion.idPartida = ");
			strcat (sql, consulta);
			strcat (sql, " AND Relacion.idPartida = Partida.Identificador;");
			mysql_query (conn, sql);
			resultado = mysql_store_result (conn); 
			row = mysql_fetch_row (resultado);
			strcpy(respuesta, row[0]);
		}
		else if (codigo ==6)
		{
/*			char *nombre[20];*/
/*			char *frase[100];*/
/*			int cant=1;*/
/*			printf("%s\n",lista->personas[0].nombre);*/
/*			if (lista->num = 1)*/
/*				strcpy  (frase, lista->personas[0].nombre);*/
/*			else*/
/*			{*/
/*				strcpy  (frase, lista->personas[0].nombre);*/
/*				while(cant < lista->num)*/
/*				{*/
/*					sprintf (nombre,"/%s",lista->personas[cant].nombre);*/
/*					strcat (frase, nombre);*/
/*					cant++;*/
/*				}*/
/*			}*/
			ListaConectados (&lista, *frase, *name, cant, x);
			strcpy (respuesta, *frase);
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
	// INICIALITZACIONS
	// Obrim el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creant socket");
	// Fem el bind al port
	
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	
	// asocia el socket a cualquiera de las IP de la m?quina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	// establecemos el puerto de escucha
	serv_adr.sin_port = htons(9020);
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
