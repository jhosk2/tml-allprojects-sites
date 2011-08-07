// GDQ �޽��� - ����.cpp : �ܼ� ���� ���α׷��� ���� �������� �����մϴ�.
//



#include "stdafx.h"



#define BUFSIZE 1024
void ErrorHandling(char* message);

int _tmain(int argc, _TCHAR* argv[]) 
{
	string IP("127.0.0.1");
	string Port("3370");


	WSADATA wsaData;
	SOCKET	hServSock;
	SOCKET	hClntSock;
	char	message[BUFSIZE];
	int strLen;

	SOCKADDR_IN servAddr;
	SOCKADDR_IN clntAddr;
	int clntAddrSize;

	/*if(argc!=2)
	{
		printf("Usage : %s <IP> <port>\n",argv[0]);
		exit(1);
	}*/

	if(WSAStartup(MAKEWORD(2,2) , &wsaData) != 0)
		ErrorHandling("WSAStartup() error!");

	hServSock = socket(PF_INET , SOCK_STREAM , 0);

	if(hServSock == INVALID_SOCKET)
		ErrorHandling("socket() error");
	
	memset(&servAddr, 0, sizeof(servAddr));
	servAddr.sin_family=AF_INET;
	servAddr.sin_addr.s_addr=inet_addr(IP.c_str());
	servAddr.sin_port=htons(atoi(Port.c_str()));

	if(bind(hServSock, (SOCKADDR*) &servAddr , sizeof(servAddr)) == SOCKET_ERROR)
		ErrorHandling("bind() error");

	if(listen(hServSock,5) == SOCKET_ERROR)
		ErrorHandling("listen() error");

	clntAddrSize = sizeof(clntAddr);
	hClntSock = accept(hServSock, (SOCKADDR*)&clntAddr, &clntAddrSize);
	if(hClntSock = INVALID_SOCKET)
		ErrorHandling("accept() error");

	while( (strLen = recv(hClntSock , message , BUFSIZE , 0)) != 0)
	{
		send(hClntSock,message,strLen,0);
	}

	closesocket(hClntSock);
	WSACleanup();

	return 0;

}

void ErrorHandling(char* message)
{
	fputs(message , stderr);
	fputc('\n',stderr);
	exit(1);
}