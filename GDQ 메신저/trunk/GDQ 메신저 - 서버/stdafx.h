// stdafx.h : ���� ��������� ���� ��������� �ʴ�
// ǥ�� �ý��� ���� ���� �� ������Ʈ ���� ���� ������
// ��� �ִ� ���� �����Դϴ�.
//

#pragma once
#pragma comment( lib , "ws2_32")

#include "targetver.h"

#define WIN32_LEAN_AND_MEAN             // ���� ������ �ʴ� ������ Windows ������� �����մϴ�.
// Windows ��� ����:
#include <windows.h>

// C�� ��Ÿ�� ��� �����Դϴ�.
#include <stdlib.h>
#include <malloc.h>
#include <memory.h>
#include <tchar.h>


// TODO: ���α׷��� �ʿ��� �߰� ����� ���⿡�� �����մϴ�.

#include <cstdio>
#include <iostream>
#include <cstdlib>
#include <locale>
#include <string>
#include <WinSock2.h>
#include <process.h>
#include <windef.h>
#include <map>
#include <vector>


using std::string;
using std::map;

typedef map<int,SOCKET>::iterator ItorIntSockMap;




// TODO: ���α׷��� �ʿ��� �߰� ����� ���⿡�� �����մϴ�.
