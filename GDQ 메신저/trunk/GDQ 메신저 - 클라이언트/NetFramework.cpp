#include "stdafx.h"
#include "NetFramework.h"

#define BUFSIZE 1024

void NetFramework::testecho(HWND hWnd)
{
	/*임시 IP, Port 선언*/
	string IP("127.0.0.1");
	string Port("3370");

	WSADATA wsaData;
	SOCKET hSocket;
	char message[BUFSIZE] = "Test Echo!";
	int strLen;

	SOCKADDR_IN servAddr;

	if(WSAStartup(MAKEWORD(2, 2), &wsaData) != 0) /* Load Winsock 2.2 DLL */
		MessageBox(hWnd, L"WSAStartup() error!", L"에러", MB_OK); 

	hSocket=socket(PF_INET, SOCK_STREAM, 0);   
	if(hSocket == INVALID_SOCKET)
		MessageBox(hWnd, L"socket() error!", L"에러", MB_OK); 

	memset(&servAddr, 0, sizeof(servAddr));
	servAddr.sin_family=AF_INET;
	servAddr.sin_addr.s_addr=inet_addr(IP.c_str());
	servAddr.sin_port=htons(atoi(Port.c_str()));

	if(connect(hSocket, (SOCKADDR*)&servAddr, sizeof(servAddr))==SOCKET_ERROR)
		MessageBox(hWnd, L"connect() error!", L"에러", MB_OK); 

	while(1) {
		Sleep(1000);
		send(hSocket, message, strlen(message), 0); /* 메시지 전송 */

		strLen=recv(hSocket, message, BUFSIZE-1, 0); /* 메시지 수신 */
		message[strLen]=0;
		printf("서버로부터 전송된 메시지 : %s \n", message);
	}

	closesocket(hSocket);
	WSACleanup();

}