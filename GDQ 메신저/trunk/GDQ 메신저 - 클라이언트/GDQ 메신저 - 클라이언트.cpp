// GDQ 메신저 - 클라이언트.cpp : 응용 프로그램에 대한 진입점을 정의합니다.
//

#include "stdafx.h"
#include "GDQ 메신저 - 클라이언트.h"
#include "NetFramework.h"

#define MAX_LOADSTRING 100

// 전역 변수:
NetFramework netframework;
HINSTANCE hInst;								// 현재 인스턴스입니다.
TCHAR szTitle[MAX_LOADSTRING];					// 제목 표시줄 텍스트입니다.
TCHAR szWindowClass[MAX_LOADSTRING];			// 기본 창 클래스 이름입니다.

WSADATA wsaData;
SOCKET hSocket;
char recvmessage[1024] = "";
int strLen;
HWND htbxClientID = NULL;
HWND htbxMessage = NULL;

// 이 코드 모듈에 들어 있는 함수의 정방향 선언입니다.
ATOM				MyRegisterClass(HINSTANCE hInstance);
BOOL				InitInstance(HINSTANCE, int);
LRESULT CALLBACK	WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK	About(HWND, UINT, WPARAM, LPARAM);
wchar_t* ConverCtoWC(char* str);
char * ConvertWCtoC(wchar_t* str);

int APIENTRY _tWinMain(HINSTANCE hInstance,
                     HINSTANCE hPrevInstance,
                     LPTSTR    lpCmdLine,
                     int       nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

 	// TODO: 여기에 코드를 입력합니다.
	MSG msg;
	HACCEL hAccelTable;

	// 전역 문자열을 초기화합니다.
	LoadString(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	LoadString(hInstance, IDC_GDQ, szWindowClass, MAX_LOADSTRING);
	MyRegisterClass(hInstance);

	// 응용 프로그램 초기화를 수행합니다.
	if (!InitInstance (hInstance, nCmdShow))
	{
		return FALSE;
	}

	hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_GDQ));

	// 기본 메시지 루프입니다.
	while (GetMessage(&msg, NULL, 0, 0))
	{
		if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg))
		{
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}
	}

	return (int) msg.wParam;
}



//
//  함수: MyRegisterClass()
//
//  목적: 창 클래스를 등록합니다.
//
//  설명:
//
//    Windows 95에서 추가된 'RegisterClassEx' 함수보다 먼저
//    해당 코드가 Win32 시스템과 호환되도록
//    하려는 경우에만 이 함수를 사용합니다. 이 함수를 호출해야
//    해당 응용 프로그램에 연결된
//    '올바른 형식의' 작은 아이콘을 가져올 수 있습니다.
//
ATOM MyRegisterClass(HINSTANCE hInstance)
{
	WNDCLASSEX wcex;

	wcex.cbSize = sizeof(WNDCLASSEX);

	wcex.style			= CS_HREDRAW | CS_VREDRAW;
	wcex.lpfnWndProc	= WndProc;
	wcex.cbClsExtra		= 0;
	wcex.cbWndExtra		= 0;
	wcex.hInstance		= hInstance;
	wcex.hIcon			= LoadIcon(hInstance, MAKEINTRESOURCE(IDI_GDQ));
	wcex.hCursor		= LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground	= (HBRUSH)(COLOR_WINDOW+1);
	wcex.lpszMenuName	= MAKEINTRESOURCE(IDC_GDQ);
	wcex.lpszClassName	= szWindowClass;
	wcex.hIconSm		= LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

	return RegisterClassEx(&wcex);
}

//
//   함수: InitInstance(HINSTANCE, int)
//
//   목적: 인스턴스 핸들을 저장하고 주 창을 만듭니다.
//
//   설명:
//
//        이 함수를 통해 인스턴스 핸들을 전역 변수에 저장하고
//        주 프로그램 창을 만든 다음 표시합니다.
//
BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
   HWND hWnd;

   hInst = hInstance; // 인스턴스 핸들을 전역 변수에 저장합니다.

   hWnd = CreateWindow(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
      CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, NULL, NULL, hInstance, NULL);

   if (!hWnd)
   {
      return FALSE;
   }

   SOCKADDR_IN servAddr;

   if(WSAStartup(MAKEWORD(2, 2), &wsaData) != 0) /* Load Winsock 2.2 DLL */
   {
	   MessageBox(hWnd, L"WSAStartup() error!", L"에러", MB_OK);
	   return FALSE; 
   }

   hSocket=socket(PF_INET, SOCK_STREAM, 0);   
   if(hSocket == INVALID_SOCKET)
   {	
	   MessageBox(hWnd, L"socket() error!", L"에러", MB_OK);
	   return FALSE;
   } 

   memset(&servAddr, 0, sizeof(servAddr));
   servAddr.sin_family=AF_INET;
   servAddr.sin_addr.s_addr=inet_addr("127.0.0.1");
   servAddr.sin_port=htons(3370);

   if(connect(hSocket, (SOCKADDR*)&servAddr, sizeof(servAddr))==SOCKET_ERROR)
   {
		MessageBox(hWnd, L"connect() error!", L"에러", MB_OK); 
		return FALSE;
   }

   ShowWindow(hWnd, nCmdShow);
   UpdateWindow(hWnd);

   return TRUE;
}

//
//  함수: WndProc(HWND, UINT, WPARAM, LPARAM)
//
//  목적: 주 창의 메시지를 처리합니다.
//
//  WM_COMMAND	- 응용 프로그램 메뉴를 처리합니다.
//  WM_PAINT	- 주 창을 그립니다.
//  WM_DESTROY	- 종료 메시지를 게시하고 반환합니다.
//
//
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	int wmId, wmEvent;
	PAINTSTRUCT ps;
	HDC hdc;

	wchar_t socmessage[1024];
	wchar_t ID[2];
	string t;

	switch (message)
	{
	case WM_CREATE:
		htbxClientID = CreateWindow(L"edit", L"", WS_VISIBLE|WS_CHILD|WS_BORDER|ES_AUTOHSCROLL|ES_AUTOVSCROLL,
			10, 10, 50, 30, hWnd, NULL, hInst, NULL);
		htbxMessage = CreateWindow(L"edit", L"", WS_VISIBLE|WS_CHILD|WS_BORDER|ES_AUTOHSCROLL|ES_AUTOVSCROLL,
			10, 50, 300, 30, hWnd, NULL, hInst, NULL);
		break;
	case WM_LBUTTONDOWN:
		GetWindowText(htbxClientID,ID,128);
		GetWindowText(htbxMessage,socmessage,128);
		t = ConvertWCtoC(ID);
		t = t + ",";
		t = t + ConvertWCtoC(socmessage);

		if(strlen(ConvertWCtoC(socmessage)) < 1)
			break;
		send(hSocket, t.c_str(), t.length(), 0); /* 메시지 전송 */
		strLen=recv(hSocket, recvmessage, 1024-1, 0); /* 메시지 수신 */
		recvmessage[strLen]=0;

		MessageBox(hWnd, ConverCtoWC(recvmessage), L"연결", MB_OK); 
		break;
	case WM_COMMAND:
		wmId    = LOWORD(wParam);
		wmEvent = HIWORD(wParam);
		// 메뉴의 선택 영역을 구문 분석합니다.
		switch (wmId)
		{
		case IDM_ABOUT:
			DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hWnd, About);
			break;
		case IDM_EXIT:
			DestroyWindow(hWnd);
			break;
		default:
			return DefWindowProc(hWnd, message, wParam, lParam);
		}
		break;
	case WM_PAINT:
		hdc = BeginPaint(hWnd, &ps);
		// TODO: 여기에 그리기 코드를 추가합니다.
		EndPaint(hWnd, &ps);
		break;
	case WM_DESTROY:
		closesocket(hSocket);
		WSACleanup();
		PostQuitMessage(0);
		break;
	default:
		return DefWindowProc(hWnd, message, wParam, lParam);
	}
	return 0;
}

// 정보 대화 상자의 메시지 처리기입니다.
INT_PTR CALLBACK About(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	UNREFERENCED_PARAMETER(lParam);
	switch (message)
	{
	case WM_INITDIALOG:
		return (INT_PTR)TRUE;

	case WM_COMMAND:
		if (LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL)
		{
			EndDialog(hDlg, LOWORD(wParam));
			return (INT_PTR)TRUE;
		}
		break;
	}
	return (INT_PTR)FALSE;
}

char * ConvertWCtoC(wchar_t* str)
{
	//반환할 char* 변수 선언
	char* pStr ; 

	//입력받은 wchar_t 변수의 길이를 구함
	int strSize = WideCharToMultiByte(CP_ACP, 0,str,-1, NULL, 0,NULL, NULL);
	//char* 메모리 할당
	pStr = new char[strSize];

	//형 변환 
	WideCharToMultiByte(CP_ACP, 0, str, -1, pStr, strSize, 0,0);
	return pStr;
}

///////////////////////////////////////////////////////////////////////
//char 에서 wchar_t 로의 형변환 함수
wchar_t* ConverCtoWC(char* str)
{
	//wchar_t형 변수 선언
	wchar_t* pStr;
	//멀티 바이트 크기 계산 길이 반환
	int strSize = MultiByteToWideChar(CP_ACP, 0,str, -1, NULL, NULL);
	//wchar_t 메모리 할당
	pStr = new WCHAR[strSize];
	//형 변환
	MultiByteToWideChar(CP_ACP, 0,str, strlen(str)+1, pStr, strSize);
	return pStr;
}