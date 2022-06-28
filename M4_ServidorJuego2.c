//-------------------Librerias---------------------------

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
//---------------------------Estructuras----------------------
//Personas
typedef struct{
	char nombre[20];
	int socket;
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

//PARTIDAS
typedef struct{
	int numJugadores;
	char ganador[20];
	//int socket;
	char resultado[100];
}Partida;
typedef struct{
	Partida partidas[100];
	int num;
}ListaPartidas;

typedef struct{
	Carta cartas[100];
	char nombreJugador[20];
	int socketJ;
}Jugador;

typedef struct{
	Jugador jugador[100];
	int num;
}ListaJugadores;


//-------------------------------- Variables Globales--------------------
ListaJugadores jogador;
ListaPartidas listaP;
ListaCartas listaC;
int puerto = 50010;
int sockets[100];
int sock_conn;
char nombre8[20];
int idP; //es el identificador de las partidas
char mensaje[512];


pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

char UsuarioPrincipal[20];
char UsuarioInvitado[20];
int socketP;

//-----------------------------Funciones Basicas ---------------------

void AddConectado (ListaPersonas *lista, int x, char usuario[20])
{
	x = lista->num;
	strcpy (lista->personas[x].nombre, usuario);
	lista->personas[x].socket = sock_conn;
	lista->num = lista->num + 1;			
	printf("Nuevo jugador conectado %s\n", lista->personas[x].nombre);
	printf("Numero de jugadores conectados : %d\n", lista->num);
}
void AddPartidaJugador (ListaJugadores *jogador1,int x, char participante[20])
{
	x = jogador1->num;
	strcpy(jogador1->jugador[x].nombreJugador, participante);
	jogador1->jugador[x].socketJ = sock_conn;
	jogador1->num++;
	printf("Nuevo jugador en la Partida %s\n",jogador1->jugador[x].nombreJugador);
	printf("Numero de jugadores en la partida : %d\n", jogador1->num);
}

void EliminarConectado (ListaPersonas *lista, char usuario[20])
{
	for (int i=0; i<lista->num; i++)
	{
		if (lista->personas[i].nombre == usuario)
		{
			strcpy (lista->personas[i].nombre, lista->personas[i+1].nombre);
			lista->personas[i].socket = lista->personas[i+1].socket;
		}			
		else
		{
			strcpy (lista->personas[i].nombre, lista->personas[i].nombre);
			lista->personas[i].socket = lista->personas[i+1].socket;
		}
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

void Conectarse(char sql[500], char consulta[100], char respuesta[100],MYSQL_RES *resultado, MYSQL_ROW row, MYSQL *conn, char usuario[100], char contra[20])
{
	int err;
	
	strcpy (sql, "SELECT Jugador.Identificador FROM (Jugador,Partida,Relacion) WHERE Jugador.Nombre = '");
	strcat (sql, usuario);
	//strcpy(UsuarioPrincipal, usuario);
	strcat (sql, "' AND Jugador.Contrasena = '");
	strcat (sql, contra); 
	strcat (sql, "'");
	printf("consulta: %s\n", sql);
	
	err = mysql_query (conn, sql);
	
	if (err!=0){
		printf("Error al introducir datos a la BBDD %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit(1);
	}
	
	
	
	
	resultado = mysql_store_result (conn); 
	row = mysql_fetch_row (resultado);
	if (row == NULL){
		sprintf(respuesta,"1/Incorrecto");
		printf ("%s\n", respuesta);
	}
	else
	{
		sprintf(respuesta,"1/Correcto");
		printf ("%s\n", respuesta);
	}	
	
	
	
	write (sock_conn,respuesta, strlen(respuesta));
	char name[20];
	char Conectados[100]; 
	int cant=1;
	ListaConectados (&lista, Conectados, name, cant);
	sprintf(respuesta,"6/%s", Conectados);			
	for (int j =0; j<lista.num; j++)
	{
		write (lista.personas[j].socket,respuesta, strlen(respuesta));
		printf("Respuesta: %s, Socket: %d\n",respuesta, lista.personas[j].socket);
	}	
	
}
void Registro(char sql[500], char consulta[100], char respuesta[100],MYSQL_RES *resultado, MYSQL_ROW row, MYSQL *conn,  char usuario[100], char contra[20],int totalj,char totaljug[100])
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




void DameDeBaja2 (char sql[500], char consulta[100], char respuesta[100],char respuesta2[100] ,MYSQL_RES *resultado,MYSQL_RES *resultado2, MYSQL_ROW row, MYSQL *conn,MYSQL *conn2, char usuario[100], char contra[20], int totalj,char totaljug[100]){
	int err;
	
	strcpy (sql, "SELECT Jugador.Identificador FROM (Jugador,Partida,Relacion) WHERE Jugador.Nombre = '");
	strcat (sql, usuario);
	//strcpy(UsuarioPrincipal, usuario);
	strcat (sql, "' AND Jugador.Contrasena = '");
	strcat (sql, contra); 
	strcat (sql, "'");
	printf("consulta: %s\n", sql);
	
	err = mysql_query (conn, sql);
	
	if (err!=0){
		printf("Error al introducir datos a la BBDD %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit(1);
	}
	
	resultado = mysql_store_result (conn); 
	row = mysql_fetch_row (resultado);
	
	if (row == NULL){
		sprintf(respuesta,"14/Incorrecto");
		printf ("%s\n", respuesta);
	}
	else
	{
		while (row != NULL){
			strcpy (sql, "DELETE FROM Jugador WHERE Jugador.Nombre = '");
			strcat (sql, usuario);
			strcat (sql, "'");
			printf("consulta = %s\n", sql);
			
			strcpy(respuesta, "14/Usuario eliminado correctamente");
			mysql_query(conn, sql);
			err = mysql_query(conn, sql);
			
			
			if (err!=0){
				printf("error al introducir datos %u %s\n", mysql_errno(conn), mysql_error(conn));
				strcpy(respuesta, "14/Usuario no se ha eliminado correctamente");
				return -1;
				exit(1);
			}
			
			printf("\n");
			printf("Despues de dar de baja al usuario la BBDD queda asi:\n");
			err=mysql_query (conn, "SELECT * FROM Jugador");
			
			if (err!=0){
				printf("error al consultar datos %u %s\n", mysql_errno(conn), mysql_error(conn));
				exit(1);
			}
			
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			
			if (row == NULL){
				printf("no se ha obtenido datos");
			}
			else
				while(row != NULL){
				printf("usuario: %s, contraseña: %s\n", row[1], row[2]);
				row = mysql_fetch_row (resultado);
				
				/*				strcpy (sql, "SELECT count(*) from Jugador");*/
				/*				mysql_query (conn2, sql);*/
				/*				resultado2 = mysql_store_result (conn2); */
				/*				row = mysql_fetch_row (resultado2);*/
				/*				strcpy(respuesta2, row[0]);*/
				/*				totalj = atoi(respuesta2);*/
				/*				totalj = totalj + 1;*/
				/*				sprintf (totaljug, "%d\n", totalj);*/
				/*				strcpy (sql, "INSERT INTO Jugador VALUES(");*/
				/*				strcat (sql, totaljug);*/
				/*				strcat (sql, ",'");*/
				/*				strcat (sql, usuario); */
				/*				strcat (sql, "','");*/
				/*				strcat (sql, contra); */
				/*				strcat (sql, "')");*/
				/*				mysql_query (conn2, sql);	*/
				/*				strcpy (respuesta2,"2/Correcto");*/
			}
				
				
				
				
				sprintf(respuesta,"14/%s\n", usuario);
				printf ("%s\n", respuesta);
				/*printf("%s\n", respuesta2);*/
				
		}
		
		
	}	
	
	
	
	write (sock_conn,respuesta, strlen(respuesta));
	/*	char name[20];*/
	/*	char Conectados[100]; */
	/*	int cant=1;*/
	/*	ListaConectados (&lista, Conectados, name, cant);*/
	/*	sprintf(respuesta,"6/%s", Conectados);			*/
	/*	for (int j =0; j<lista.num; j++)*/
	/*	{*/
	
	/*		write (lista.personas[j].socket,respuesta, strlen(respuesta));*/
	/*		printf("Respuesta: %s, Socket: %d\n",respuesta, lista.personas[j].socket);*/
	/*	}	*/
}


//--------------------------Funciones Juego--------------------------
void DameCartas(char respuesta[100], int numForm, ListaPartidas lista)
{
	listaC.num = 0;
	int i = 0;//Primero tenemos que ver cuantoa jugadores tenemos para posteriormente repartir cartas
	//las 5 primeras cartas son de la mesa siempre seran las de la mesa
	
	int k = 0;
	
	
	srand(time(NULL));
	int cartaVerificadora[100]; //Ponemos valores valores imposibles para que no hagan nada en el primer intento
	int cartaVerificadora2[100];
	printf("Correcto \n");
	sprintf(respuesta, "7/%d", numForm); //en vez de 1 va numform
	
	while (i < 2 + 5) //AQUI IRÍA EL NUMERO DE JUGADORES EN VEz DE 2, EL % ES EL NUMERO MINIMO
	{
		listaC.cartas[i].valor = rand()%13; //es 1 más del número que queremos generar
		listaC.cartas[i].palo = rand()%4;
		
		int j = 0;
		
		while (j < listaC.num)
		{
			/*			listaC.cartas[i].valor = rand()%13; *///Generamos las veces que haga falta para que no se repita
			/*			listaC.cartas[i].palo = rand()%4;*/
			if((listaC.cartas[i].valor ==listaC.cartas[j].valor) && (listaC.cartas[i].palo == listaC.cartas[j].palo))
			{
				listaC.cartas[i].valor = rand()%13; //Generamos las veces que haga falta para que no se repita
				listaC.cartas[i].palo = rand()%4;
			}
			
			j++;
			
		}
		printf("Numero: %d , palo: %d \n",listaC.cartas[i].valor, listaC.cartas[i].palo);	
		
		//cartaVerificadora[i] = listaC.cartas[i].valor;
		//cartaVerificadora2[i] = listaC.cartas[i].palo;
		listaC.num++;
		i++;
		//j++;
	}
	while( k < listaC.num)
	{
		sprintf(respuesta, "%s/%d/%d",respuesta,listaC.cartas[k].palo,listaC.cartas[k].valor);
		k++;
	}
}
void Partida_poker(char resultadoPartida[100], int numForm,char respuesta[100], ListaPartidas listaP)
{
	listaC.num = 0;
	int i = 0;//Primero tenemos que ver cuantoa jugadores tenemos para posteriormente repartir cartas
	//las 5 primeras cartas son de la mesa siempre seran las de la mesa
	
	int k = 0;
	
	
	srand(time(NULL));
	printf("Correcto \n");
	sprintf(resultadoPartida, "10/%d", numForm); //en vez de 1 va numform
	
	while (i < (listaP.partidas[idP].numJugadores)*2 + 5) //AQUI IRÍA EL NUMERO DE JUGADORES EN VEz DE 2, EL % ES EL NUMERO MINIMO, es por 2 porque cada jugador tiene 2 cartas
	{
		listaC.cartas[i].valor = rand()%13; //es 1 más del número que queremos generar
		listaC.cartas[i].palo = rand()%4;
		
		int j = 0;
		
		while (j < listaC.num)
		{
			/*			listaC.cartas[i].valor = rand()%13; *///Generamos las veces que haga falta para que no se repita
			/*			listaC.cartas[i].palo = rand()%4;*/
			if((listaC.cartas[i].valor ==listaC.cartas[j].valor) && (listaC.cartas[i].palo == listaC.cartas[j].palo))
			{
				listaC.cartas[i].valor = rand()%13; //Generamos las veces que haga falta para que no se repita
				listaC.cartas[i].palo = rand()%4;
			}
			
			j++;
			
		}
		printf("Numero: %d , palo: %d \n",listaC.cartas[i].valor, listaC.cartas[i].palo);	
		
		//cartaVerificadora[i] = listaC.cartas[i].valor;
		//cartaVerificadora2[i] = listaC.cartas[i].palo;
		listaC.num++;
		i++;
		//j++;
	}
	while( k < listaC.num)
	{
		sprintf(resultadoPartida, "%s/%d/%d",resultadoPartida,listaC.cartas[k].palo,listaC.cartas[k].valor);
		k++;
	}
	printf("%s \n", resultadoPartida);
	printf("Hecho \n");
	
	//EN teoria tendremos: 10 numeros que son 5 cartas para la mesa
	//luego tendremos 2 numeros por cada jugador
}
void DameGanadorDePartida(char resultadoPartida[100],int numForm, ListaPartidas listaP,char respuesta[100] )
{
	
	printf("Vamos a ver los resultados \n");
	int i = 1;
	int j = 0;
	int k = 0;
	int parejas;
	int trios;
	int poker;
	//int Deque; //entero que recoge el valor de la carta que hace pareja, trio o poker
	int full;
	int palo[100];
	int valor[100];
	
	
	
	
	char *p;
	printf("Vamos a ver los resultados \n");
	//OJO ESTA CARTA TAMBIEN ES DE LA MESA
	p = strtok(resultadoPartida, "/");
	jogador.jugador[0].cartas[0].palo = atoi(p);
	
	p = strtok(NULL, "/");
	jogador.jugador[0].cartas[0].valor= atoi(p);
	int m = 1; //m = jugador  jugador numero 1 es la mesa
	int n = 0;
	printf("%d y %d \n", jogador.jugador[0].cartas[0].palo, jogador.jugador[0].cartas[0].valor);
	
	
	while (i <= 4) //Miramos las cartas de la mesa //la mesa sera el jugador 1 
	{
		printf("Miramos las cartas de la mesa \n");
		p = strtok(NULL, "/");
		jogador.jugador[m].cartas[i].palo = atoi(p);
		
		p = strtok(NULL, "/");
		jogador.jugador[m].cartas[i].valor = atoi(p);
		printf("Carta numero %d es palo: %d valor: %d \n",i, jogador.jugador[m].cartas[i].palo, jogador.jugador[m].cartas[i].valor);
		
		i++;
	}
	m++;
	printf("NUMERO DE JUGADORES: %d \n",listaP.partidas[idP].numJugadores);
	while( i <= 4 + (listaP.partidas[idP].numJugadores)*2) //miramos las cartas de cada jugador 2 y 3
	{
		p = strtok(NULL, "/");
		printf( "Palo: %d \n",atoi(p));
		jogador.jugador[m].cartas[i].palo = atoi(p);
		
		p = strtok(NULL, "/");
		jogador.jugador[m].cartas[i].valor = atoi(p);
		printf( "Valor: %d \n",atoi(p));
		printf("n: %d \n",n);
		printf("m: %d \n",m);
		//Si repartimos 2 cartas pasamos al siguiente jugador
		
		printf("1-Jugador: %d con Carta numero %d es palo: %d valor: %d \n",m,i, jogador.jugador[m].cartas[i].palo, jogador.jugador[m].cartas[i].valor);
		i++;
		if (n == 2)
		{
			printf("M: %d \n",m);
			m++;
			n= 0;
		}
		n++;
	}
	
	//AHORA NOS TOCA ver Resultados
	int o = 0;
	while ( o <  (listaP.partidas[idP].numJugadores)*2 + 5)
	{
		printf("CORRECTO \n");
		
		while (k < (listaP.partidas[idP].numJugadores)*2 + 5) //K = numero de jugador
		{
			while (j < (listaP.partidas[idP].numJugadores)*2 + 5)
			{
				if(jogador.jugador[k].cartas[o].valor == jogador.jugador[k].cartas[j].valor)
				{
					parejas++;
					printf("parejas %d \n",parejas);
					//Deque =  jogador.jugador[k].cartas[j].valor
				}
				if (parejas == 2)
				{
					trios++;
					printf("trios %d",trios);
					
				}
				if (parejas == 3)
				{
					trios = 0;
					printf("poker %d",poker);
					poker++;
					parejas = 0;
				}
				j++;
			}
			k++;
		}
		o++;
	}
	int l = 0;
	while ( l < (listaP.partidas[idP].numJugadores)*2 + 5)
	{
		if (poker >= 1)
		{
			//printf("Poker de %d",Deque);
			sprintf(respuesta,"9/poker/%d \n",trios);
			printf("%s TRIO \n",respuesta);
		}
		if  ((trios >= 1) && (parejas ==0))
		{
			//printf("trio de %d",Deque);			
			sprintf(respuesta,"9/trio/%d \n",trios);
			printf("%s TRIO\n",respuesta);
		}
		if  ((parejas >= 1) && (trios ==0))
		{
			//printf("Pareja de %d",Deque);			
			sprintf(respuesta,"9/pareja/%d \n",parejas);
			printf("%s PAREJA \n",respuesta);
		}
		if  ((parejas >= 1) && (trios >= 1))
		{
			full++;
			//printf("full de %d",Deque);
			sprintf(respuesta,"9/full/%d \n",full);
			printf("%s FULL \n",respuesta);
			
		}
		l++;
		
	}
	
	
}

void chat (char respuesta[512],char usuario[200], char mensaje[512] ,MYSQL *conn, ListaPersonas *lista){
	
	
	int x;
	int encontrado = 0;
	while ((x < lista->num) && (!encontrado)){
		if (lista->personas[x].socket == sockets)
			encontrado = 1;
		else 
			x++;
		if (encontrado){
			usuario = lista->personas[x].nombre;
		}
	}
	
	
	sprintf(respuesta, "13/%s/%s\n", usuario, mensaje);
	
	
	
}


//------------------------- Consultas BBDD------------------------

void DamePuntos(char sql[500], char consulta[100], char respuesta[100],MYSQL_RES *resultado, MYSQL_ROW row, MYSQL *conn)
{
	
	strcpy (sql, "SELECT Relacion.Puntos FROM (Jugador,Partida,Relacion) WHERE Jugador.Nombre = '");
	strcat (sql, consulta);
	strcat (sql, "' AND Relacion.idJugador = Jugador.Identificador");
	mysql_query (conn, sql);
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	//sprintf(respuesta,"3/%d/%s",numForm,row[0]);
	sprintf(respuesta,"3/%s",row[0]);
}	

void DameFecha(char sql[500], char consulta[100], char respuesta[100],MYSQL_RES *resultado, MYSQL_ROW row, MYSQL *conn)
{
	strcpy (sql, "SELECT Partida.Fecha FROM (Partida) WHERE Partida.Identificador = ");
	strcat (sql, consulta);
	mysql_query (conn, sql);
	resultado = mysql_store_result (conn); 
	row = mysql_fetch_row (resultado);
	sprintf(respuesta,"4/%s",row[0]);
}
void DameGanador(char sql[500], char consulta[100], char respuesta[100],MYSQL_RES *resultado, MYSQL_ROW row, MYSQL *conn)
{
	strcpy (sql, "SELECT Jugador.Nombre FROM (Jugador,Partida,Relacion) WHERE Relacion.Puntos = 3 AND Relacion.idJugador = Jugador.Identificador AND Relacion.idPartida = ");
	strcat (sql, consulta);
	strcat (sql, " AND Relacion.idPartida = Partida.Identificador;");
	mysql_query (conn, sql);
	resultado = mysql_store_result (conn); 
	row = mysql_fetch_row (resultado);
	sprintf(respuesta,"5/%s",row[0]);
}
void DameListaConectados(char respuesta[100])
{
	char name[20];
	char Conectados[100]; //Se guarda un string con todos los conectados
	int cant=1;
	ListaConectados (&lista, Conectados, name, cant);
	sprintf(respuesta,"6/%s",Conectados);	
}

int DameDeBaja(char usuario[200], char contra[200], char sql[500],char consulta[100],char respuesta[100] , MYSQL_RES *resultado, MYSQL_ROW row, MYSQL *conn){
	
	
	strcpy (sql, "SELECT Jugador.Identificador FROM (Jugador,Partida,Relacion) WHERE Jugador.Nombre = '");
	strcat (sql, usuario);
	//strcpy(UsuarioPrincipal, usuario);
	strcat (sql, "' AND Jugador.Contrasena = '");
	strcat (sql, contra); 
	strcat (sql, "'");
	printf("consulta: %s\n", sql);
	/*	strcpy (sql, "SELECT Jugador.Nombre FROM (Jugador) WHERE Jugador.Nombre ='");*/
	/*	strcat (sql, usuario);*/
	/*	printf("%s\n", usuario);*/
	/*	strcat (sql, "'");*/
	/*	strcat(sql, " AND Jugador.Contrasena='");*/
	/*	strcat(sql, contra);*/
	/*	printf("%s\n", contra);*/
	/*	strcat(sql, "';");*/
	
	printf("consulta == %s\n", sql);
	
	/*	strcpy (sql, "SELECT Jugador.Nombre FROM Jugador WHERE Jugador.Nombre = 'a' AND Jugador.Contrasena = 'a'");*/
	int err = mysql_query(conn, sql);
	
	if (err!=0){
		printf("el usuario y la contraseña no coinciden %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit(1);
	}
	
	if (row == NULL){
		printf("El usuario y la contraseña no coinciden\n");
		strcpy(respuesta, "El usuario No existe");
		return -1;
	}
	else{
		while (row != NULL){
			strcpy (sql, "DELETE FROM Jugador WHERE Jugador.Nombre = '");
			strcat (sql, usuario);
			strcat (sql, "'");
			printf("consulta = %s\n", sql);
			
			strcpy(respuesta, "14/Usuario eliminado correctamente");
			mysql_query(conn, sql);
			err = mysql_query(conn, sql);
			
			if (err!=0){
				printf("error al introducir datos %u %s\n", mysql_errno(conn), mysql_error(conn));
				strcpy(respuesta, "14/Usuario no se ha eliminado correctamente");
				return -1;
				exit(1);
			}
			
			printf("\n");
			printf("Despues de dar de baja al usuario la BBDD queda asi:\n");
			err=mysql_query (conn, "SELECT * FROM Jugador");
			
			if (err!=0){
				printf("error al consultar datos %u %s\n", mysql_errno(conn), mysql_error(conn));
				exit(1);
			}
			
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			
			if (row == NULL){
				printf("no se ha obtenido datos");
			}
			else
				while(row != NULL){
				printf("usuario: %s, contraseña: %s\n", row[1], row[2]);
				row = mysql_fetch_row (resultado);
			}
				
				return;	
		}			
	}			
}


// ------------------------- Atender Cliente ----------------------------

void *AtenderCliente (void *socket)
{
	int sock_conn;
	int *s;
	s= (int *) socket;
	sock_conn= *s;
	int terminar = 0;
	
	//Conexion a la base de datos
	MYSQL *conn;
	MYSQL *conn2;
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_RES *resultado2;
	MYSQL_ROW row;	
	//Creamos una conexion al servidor MYSQL 
	conn = mysql_init(NULL);
	if (conn==NULL) {
		printf ("Error al crear la conexi??n: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	//inicializar la conexion
	conn = mysql_real_connect (conn, "shiva2.upc.es","root", "mysql", "M4_Juego",0, NULL, 0);
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
	resultado2 = mysql_store_result (conn);
	//Finalizacion de la conexion con la base de datos
	printf("Conexion a la base de datos correcta\n");
	
	int sock_listen, ret;
	struct sockaddr_in serv_adr;
	char peticion[512];
	char respuesta[512];
	char respuesta2[512];
	int numForm;
	
	int i;
	while (terminar ==0)
	{
		
		// Ahora recibimos la petici?n
		printf("espero: %d \n",sock_conn);
		ret=read(sock_conn,peticion, sizeof(peticion));
		
		
		printf("recibido: %d  %s \n",sock_conn, peticion);
		// Tenemos que a?adirle la marca de fin de string 
		// para que no escriba lo que hay despues en el buffer
		//peticion[ret]='\0';
		
		
		printf ("Peticion: %s\n",peticion);
		
		peticion[ret]='\0';
		
		if(strcmp(peticion,"")==0)
			strcpy(peticion,"17/nada");
		
		// vamos a ver que quieren
		char *p = strtok( peticion, "/");
		int codigo =  atoi (p);
		printf("Codigo: %d \n",codigo);
		printf("El ret es: %d \n",ret);
		// Ya tenemos el c?digo de la petici?n
		
		
		
		char usuario[20];
		char contra[20];
		char nombre[20];
		char consulta[20];
		int x;
		if ((codigo == 7) || (codigo == 10)||(codigo == 8) || (codigo == 9))
		{
			p = strtok(NULL, "/");
			numForm = atoi(p);
			printf("numero de formulario: %d\n",numForm);
			printf("MACARRONES \n");
		}
		if ((codigo ==1) || (codigo ==2))
		{
			p = strtok( NULL, "/");				
			strcpy (usuario, p);
			p = strtok( NULL, "/");				
			strcpy (contra, p);				
			printf ("Codigo: %d, Usuario: %s, Contraseña: %s\n", codigo, usuario, contra);
			AddConectado(&lista, x, usuario);		
		}			
		else if  ((codigo ==3) || (codigo ==4) || (codigo ==5) || (codigo ==6) || (codigo== 9))
		{
			p = strtok( NULL, "/");				
			strcpy (consulta, p);
			printf ("Codigo: %d, Consulta: %s\n", codigo, consulta);
		}
		else if (codigo ==8)
		{
			p = strtok( NULL, "/");				
			strcpy (consulta, p);
			printf ("Codigo: %d, Consulta: %s\n", codigo, consulta);
			p = strtok (NULL, "/");
			strcpy(UsuarioPrincipal,p);
			printf("EL JUGADOR NUMERO 1 ES: %s\n", UsuarioPrincipal);
		}
		if (codigo ==9)
		{
			p = strtok (NULL, "/");
			strcpy(UsuarioInvitado,p);
			printf("EL JUGADOR INVITADO ES: %s\n",UsuarioInvitado);
		}
		
		//char nombre8[20];
		int totalj;
		char totaljug[2];
		char sql[100];
		if (codigo ==0)
		{
			terminar=1;
			EliminarConectado (&lista, usuario);
			char name[20];
			char Conectados[100]; 
			int cant=1;
			ListaConectados (&lista, Conectados, name, cant);
			sprintf(respuesta,"6/%s\n", Conectados);
			for (int j =0; j<lista.num; j++)
			{
				write (sockets[j],respuesta, strlen(respuesta));
			}
			
		}			
		else if (codigo ==1) 
		{
			//Conectarse(sql,consulta,respuesta,resultado,row,conn,usuario,contra,numForm);
			Conectarse(sql,consulta,respuesta,resultado,row,conn,usuario,contra);
		}
		else if (codigo ==2)
		{
			//Desconectarse(sql,consulta,respuesta,resultado,row,conn,usuario,contra,totalj,totaljug,numForm);
			Registro(sql,consulta,respuesta,resultado,row,conn,usuario,contra,totalj,totaljug);
			
		}
		else if (codigo ==3)
		{			
			//DamePuntos(sql,consulta,respuesta,resultado,row,conn,numForm);
			
			DamePuntos(sql,consulta,respuesta,resultado,row,conn);
		}
		else if (codigo ==4)
		{
			DameFecha(sql,consulta,respuesta,resultado,row,conn);
			//DameFecha(sql,consulta,respuesta,resultado,row,conn,numForm);
		}
		else if (codigo ==5)
		{
			DameGanador(sql,consulta,respuesta,resultado,row,conn);
			//DameGanador(sql,consulta,respuesta,resultado,row,conn,numForm);
		}
		else if (codigo ==6)
		{
			DameListaConectados(respuesta);
			//DameListaConectados(respuesta, numForm);
		}
		
		else if (codigo ==7) 
		{
			DameCartas(respuesta,numForm, listaP);
		}
		else if (codigo == 8)
		{
			//strcpy(nombre8,"QUESO \n");			
			sprintf(respuesta,"8/%d/%s\n",numForm,consulta);
			printf("8/%d/%s\n",numForm,consulta);
			int x;
			printf("LA CONSULTA ES: %s\n",consulta);
			
			
			for (x =0; x<lista.num; x++)
			{
				if (strcmp(consulta,lista.personas[x].nombre) == 0)	
				{
					write (lista.personas[x].socket,respuesta, strlen(respuesta));
					strcpy (nombre8, lista.personas[x].nombre);
					printf("EL NOMBRE ES: %s\n",nombre8);
					printf("Respuesta: %s, Socket: %d\n",respuesta, lista.personas[x].socket);
				}
			}
			//strcpy(UsuarioPrincipal,consulta);
			
		}
		else if (codigo == 9)
		{
			sprintf(respuesta,"9/%d/%s\n",numForm,consulta);
			printf("LA RESPUESTA ES 1: %s\n",respuesta);
			printf("LA CONSULTA ES: %s\n",consulta);
			printf("EL NOMBRE ES: %s\n",nombre8);
			int t = 1;
			
			//AddPartidaJugador(&jogador,t, UsuarioPrincipal);
			for(x =0; x<lista.num; x++)
			{
				if (strcmp(nombre8,lista.personas[x].nombre) == 0)					
				{
					write (lista.personas[x].socket,respuesta, strlen(respuesta));
					printf("Respuesta: %s, Socket: %d\n",respuesta, lista.personas[x].socket);
				}
			}
			int NumeroDeJugador = 1;
			int g = 0;
			char RespuestaNumeroDeJugador[100];
			strcpy(jogador.jugador[NumeroDeJugador].nombreJugador, UsuarioPrincipal);
			while( g < lista.num) //Buscamos el jugador principal para ponerle el socket correspondiente
			{
				if(strcmp(lista.personas[g].nombre,UsuarioPrincipal)==0) 
				{
					jogador.jugador[NumeroDeJugador].socketJ = lista.personas[g].socket;
				}
				g++;
			}
			
			sprintf(RespuestaNumeroDeJugador, "11/%d/%d/\n",numForm,NumeroDeJugador);
			
			for(x =1; x<=lista.num; x++)
			{
				if (strcmp(UsuarioPrincipal,jogador.jugador[x].nombreJugador) == 0)	
				{
					printf("Respuesta enviada: %s\n",RespuestaNumeroDeJugador);
					write (jogador.jugador[x].socketJ,RespuestaNumeroDeJugador, strlen(RespuestaNumeroDeJugador));
					printf("Notificacion de Jugadores 1: %s, Socket: %d\n",RespuestaNumeroDeJugador,jogador.jugador[x].socketJ);
				}
			}
			printf("Nuevo jugador conectado: %s , con socket: %d\n",jogador.jugador[NumeroDeJugador].nombreJugador, jogador.jugador[NumeroDeJugador].socketJ);
			//jogador.num++;
			if (strcmp(consulta,"SI")==0)
			{
				t++;
				//AddPartidaJugador(&jogador,t,UsuarioInvitado);
				int f = 0;
				while (f < lista.num)
				{
					printf("JUGADOR DISPONIBLE numero: %d es %s :\n",f,lista.personas[f].nombre);
					f++;
				}
				
				NumeroDeJugador++;
				//jogador.num ++;
				//sprintf(RespuestaNumeroDeJugador, "11/%d/%d",numForm,NumeroDeJugador);
				sprintf(RespuestaNumeroDeJugador, "11/%d/%d\n",numForm,NumeroDeJugador);
				printf("Respuesta Jugador: %s\n",RespuestaNumeroDeJugador);
				
				
				/*				char RespuestaNumeroDeJugador2[100];*/
				/*				sprintf(RespuestaNumeroDeJugador2,"11/%d/%d",numForm,NumeroDeJugador);*/
				/*				printf("Respuesta Numero de jugadorffffffffffffffffffffffffffff: %s \n",RespuestaNumeroDeJugador2);*/
				jogador.num = NumeroDeJugador;
				strcpy(jogador.jugador[NumeroDeJugador].nombreJugador, UsuarioInvitado);
				printf("El usuario invitado es %s\n", jogador.jugador[NumeroDeJugador].nombreJugador);
				printf("JUGADOR de usuario principal es : %s\n",UsuarioPrincipal);
				
				//jogador.num++;
				printf("jogador.num = %d\n",jogador.num);
				printf("Numero de jugador: %d\n",NumeroDeJugador);
				int n= 1;
				while( n <= lista.num)
				{
					printf("jugadores  en la partida\n");
					printf("%s\n",jogador.jugador[n].nombreJugador);
					n++;
				}
				
				int g = 0;
				while( g < lista.num) //Buscamos el jugador principal para ponerle el socket correspondiente
				{
					printf("Nombres: %s\n",lista.personas[g].nombre);
					if(strcmp(lista.personas[g].nombre,UsuarioInvitado)==0) 
					{
						jogador.jugador[NumeroDeJugador].socketJ = lista.personas[g].socket;
					}
					g++;
				};
				
				printf("Jugador invitado: %s\n",UsuarioInvitado);
				
				//jogador.num ++;
				//lista.num++;
				int p = 0;
				printf("lista.num %d\n",lista.num);
				while (p < lista.num)
				{
					printf("Jugador de la lista.num: %d es %s\n ",p,lista.personas[p].nombre);
					p++;
				}
				for(x =1; x<=lista.num; x++)
				{
					printf("Nombre jugador: %s\n",jogador.jugador[x].nombreJugador);
					if (strcmp(UsuarioInvitado,jogador.jugador[x].nombreJugador) == 0)	
					{
						printf("Respuesta enviada: %s\n",RespuestaNumeroDeJugador);
						write (jogador.jugador[x].socketJ,RespuestaNumeroDeJugador, strlen(RespuestaNumeroDeJugador));
						printf("Notificacion de Jugadores 1: %s, Socket: %d\n",RespuestaNumeroDeJugador,jogador.jugador[x].socketJ);
					}
				}
				printf("-Nuevo jugador conectado: %s , con socket: %d\n",jogador.jugador[NumeroDeJugador].nombreJugador, jogador.jugador[NumeroDeJugador].socketJ);
				//jogador.num ++;
				listaP.num++;
				idP = listaP.num;	
				listaP.partidas[idP].numJugadores = listaP.partidas[idP].numJugadores + 2;
				char resultadoPartida[100];
				char abc[100]; //No se porque pero si pongo este char aqui resultado partida funciona correctamente
				//printf("ER PEPE: %s \n", abc);
				Partida_poker(resultadoPartida, numForm, respuesta,listaP);
				//Partida_poker(resultadoPartida, numForm, respuesta,jogador);
				int x;
				//char respuesta1[100];
				//char dcd[100];
				//strcpy(respuesta1, resultadoPartida);
				printf("LAS CARTAS %s\n", resultadoPartida);
				strcpy ( respuesta, resultadoPartida);
				n= 0;
				while( n < lista.num)
				{
					printf("jugadores  en la partida\n");
					printf("%s\n",jogador.jugador[n].nombreJugador);
					printf("Con sockets %d\n", jogador.jugador[n].socketJ);
					n++;
				}
				for (x =1; x<=lista.num; x++)
				{
					write (jogador.jugador[x].socketJ,respuesta, strlen(respuesta));
					printf("Respuesta enviada a : %s\n",jogador.jugador[x].nombreJugador);
					printf("Respuesta 1: %s, Socket: %d\n",respuesta, jogador.jugador[x].socketJ);
					
				}
				printf("Terminado %d \n",sock_conn);
				
			}
			strcpy(peticion,"17/nada");
			
		}
		else if (codigo == 10)//JugarPartida
		{
			/*			p = strtok(NULL, "/");*/
			/*			numForm = atoi(p);*/
			
			/*			char resultadoPartida[100];*/
			/*			Partida_poker(resultadoPartida, numForm, respuesta,listaP);*/
			
			/*			DameGanadorDePartida(resultadoPartida, numForm, listaP, respuesta);*/
			
			/*			int x;*/
			/*			for (x =0; x<lista.num; x++)*/
			/*			{*/
			/*				if (strcmp(consulta,lista.personas[x].nombre) == 0)	*/
			/*				{*/
			/*					write (lista.personas[x].socket,respuesta, strlen(respuesta));*/
			/*					strcpy (nombre8, lista.personas[x].nombre);*/
			/*					printf("Respuesta: %s, Socket: %d\n",respuesta, lista.personas[x].socket);*/
			/*				}*/
			/*			}*/
		}
		else if (codigo == 13){
			
			
			
			
			p = strtok (NULL, "/");
			strcpy (usuario, p);
			printf("Usuario: %s \n", usuario);
			
			p = strtok (NULL, "/");
			strcpy (mensaje, p);
			printf(" Mensaje: %s \n", mensaje);
			
			//pthread_mutex_lock(&mutex);
			chat(respuesta, usuario, mensaje, conn, &lista);
			//pthread_mutex_unlock(&mutex);
			int i=0;
			for (i=0; i < lista.num; i++){
				write (lista.personas[i].socket, respuesta, strlen (respuesta));
			}
			
		}		
		else if (codigo == 14){
			
			DameDeBaja2(sql,consulta,respuesta, respuesta2,resultado,resultado2,row,conn,conn2,usuario,contra,totalj,totaljug);
			
			/*			p = strtok (NULL, "/");*/
			/*			strcpy (usuario, p);*/
			/*			printf(" Usuario: %s \n", usuario);*/
			
			/*			p = strtok (NULL, "/");*/
			/*			strcpy (contra, p);*/
			/*			printf(" Contra: %s \n", contra);*/
			
			
			/*			int res = DameDeBaja(usuario, contra, sql, consulta, respuesta, resultado, row, conn);*/
			/*			if (res == 0){*/
			/*				write (sock_conn, respuesta, strlen(respuesta));*/
			/*			}*/
			/*			else*/
			/*				write (sock_conn, respuesta, strlen(respuesta));*/
		}
		else if (codigo == 17)
		{
			printf("No sirve para nada pero arregla el problema\n");
		}
		
		if ((codigo !=0) && (codigo !=1) && (codigo !=6) && (codigo !=8) && (codigo !=9) && (codigo != 10) &&(codigo !=11) && (codigo !=13) && (codigo !=17))
		{				
			printf("Codigo: %d\n", codigo);
			printf ("Respuesta: %s\n", respuesta);
			// Enviamos respuesta
			write (sock_conn,respuesta, strlen(respuesta));
		}
		//}
	}
	// Se acabo el servicio para este cliente
	close(sock_conn);
	mysql_close(conn);
	
}








//---------------------------------- Main ------------------------------

int main(int argc, char *argv[])
{
	int sock_listen, ret;
	struct sockaddr_in serv_adr;
	char peticion[512];
	char respuesta[512];
	
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
	
	
	//int sockets[100];
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
