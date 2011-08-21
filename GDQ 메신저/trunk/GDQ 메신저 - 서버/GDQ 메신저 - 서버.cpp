// GDQ �޽��� - ����.cpp : �ܼ� ���� ���α׷��� ���� �������� �����մϴ�.
//asd



#include "stdafx.h"



#define BUFSIZE 100

void ErrorHandling(char* message);
DWORD32 WINAPI ClientConn(void* arg);
void SendMSG(char* message, int len);

int clntNumber = 0;
SOCKET clntSocks[10];
HANDLE hMutex;

//map<int,SOCKET> mClntSock;
map<string,SOCKET> mClntSock;
int		nCount = 0;

int _tmain(int argc, _TCHAR* argv[]) 
{
	string IP("127.0.0.1");
	string Port("3370");


	WSADATA wsaData;
	SOCKET	hServSock;
	SOCKET	hClntSock;
	

	SOCKADDR_IN servAddr;
	SOCKADDR_IN clntAddr;
	int clntAddrSize;

	HANDLE hThread;
	DWORD	dwThreadID;

	/*if(argc!=2)
	{
		printf("Usage : %s <IP> <port>\n",argv[0]);
		exit(1);
	}*/

	if(WSAStartup(MAKEWORD(2,2) , &wsaData) != 0)
		ErrorHandling("WSAStartup() error!");

	hMutex = CreateMutex(NULL,FALSE,NULL);
	if(hMutex == NULL)
	{
		ErrorHandling("CreateMutex() error");
	}

	hServSock = socket(PF_INET , SOCK_STREAM , 0);

	if(hServSock == INVALID_SOCKET)
		ErrorHandling("socket() error");
	
	memset(&servAddr, 0, sizeof(servAddr));
	servAddr.sin_family=AF_INET;
	servAddr.sin_addr.s_addr=inet_addr(IP.c_str());
	servAddr.sin_port =htons(atoi(Port.c_str()));

	if(bind(hServSock, (SOCKADDR*) &servAddr , sizeof(servAddr)) == SOCKET_ERROR)
		ErrorHandling("bind() error");

	if(listen(hServSock,5) == SOCKET_ERROR)
		ErrorHandling("listen() error");

	while(1)
	{
		clntAddrSize = sizeof(clntAddr);
		hClntSock = accept(hServSock, (SOCKADDR*)&clntAddr, &clntAddrSize);

		if(hClntSock == INVALID_SOCKET)
			ErrorHandling("accept() error");

		WaitForSingleObject(hMutex,INFINITE);
		//clntSocks[clntNumber++] = hClntSock;
		
		//mClntSock[clntNumber] = hClntSock;
		
		//clntNumber ++;

		ReleaseMutex(hMutex);

		printf("�� ���� , Ŭ���̾�Ʈ IP : %s \n",inet_ntoa(clntAddr.sin_addr));

		hThread = (HANDLE)_beginthreadex(NULL,0,ClientConn,(void*)hClntSock,0,(unsigned*)&dwThreadID);

		if(hThread == 0)
		{
			ErrorHandling("Thread Create Error");
		}
	}

	/*
	

	closesocket(hClntSock);
	*/
	
	WSACleanup();

	return 0;

}

void ErrorHandling(char* message)
{
	fputs(message , stderr);
	fputc('\n',stderr);
	exit(1);
}

DWORD32 WINAPI ClientConn(void* arg)
{
	SOCKET	clntSock	= (SOCKET)arg;
	int		strLen		= 0;
	char	message[BUFSIZE];
	string ID;
	
	
	memset(message,NULL,BUFSIZE);

	//÷�� �޼��� ����
	strLen = recv(clntSock , message , BUFSIZE , 0);

	// �޼��� ����ȯ 
	ID = message;

	unsigned int a = mClntSock[ID];

	if(mClntSock[ID] != NULL) // �ߺ� üũ 
	{
		// �ߺ��̸� Fail �޼��� ������ ���� ����
		memset(message,NULL,BUFSIZE);
		strcpy(message,"Fail");

		send(clntSock,message,strlen(message),0);

		closesocket(clntSock);

		return 0;
	}

	//�� ������ �ߺ� �ƴ�
	
	// ���̵� �߰�
	mClntSock[ID] = clntSock;
	
	// �޼��� OK �޼��� ���� 
	memset(message,NULL,BUFSIZE);
	strcpy(message,"OK");

	send(clntSock,message,strlen(message),0);

	while( (strLen = recv(clntSock , message , BUFSIZE , 0)) != 0)
	{
		
		WaitForSingleObject(hMutex,INFINITE);

		char* ptemp = strchr(message,',');
		int nPos = strlen(message) - strlen(ptemp);
		char ptemp2[100];				

		strncpy(ptemp2,message,nPos);

		// b = message���� �ɷ��� ���̵�
		string nRecvID = ptemp2;

		// temp3 = message���� �ɷ��� ���� ����
		char* pRecvMessage = &message[nPos+1];

		if(mClntSock[nRecvID])
			send(mClntSock[nRecvID],pRecvMessage,strlen(pRecvMessage),0);		

		ReleaseMutex(hMutex);
	}

	WaitForSingleObject(hMutex,INFINITE);

	
	for(ItorIntSockMap itor = mClntSock.begin()
			; itor != mClntSock.end()
			; ++itor)
	{
		if(clntSock == itor->second)
		{
			mClntSock.erase(itor);
			break;
		}
	}
	
	
	ReleaseMutex(hMutex);

	closesocket(clntSock);

	return 0;

}

void SendMSG(char* message, int len)
{
	WaitForSingleObject(hMutex,INFINITE);
	
	for(int i = 0 ; i < clntNumber ; i++)
	{
		send(clntSocks[i],message,len,0);
		
	}
	
	ReleaseMutex(hMutex);

}

