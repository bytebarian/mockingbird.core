#include "StdAfx.h"
#include "DataStructure.h"
#include "Utility.h"

#include "stdafx.h"
#include <string>
#include <iostream>
#include <fstream>

using namespace std;

DotNetVersion g_tDotNetVersion;
HMODULE g_hJitModule = NULL;
HMODULE g_hClrModule = NULL;

MethodDesc::PFN_Reset MethodDesc::s_pfnReset = NULL;
MethodDesc::PFN_IsGenericMethodDefinition MethodDesc::s_pfnIsGenericMethodDefinition = NULL;
MethodDesc::PFN_GetNumGenericMethodArgs MethodDesc::s_pfnGetNumGenericMethodArgs = NULL;
MethodDesc::PFN_StripMethodInstantiation MethodDesc::s_pfnStripMethodInstantiation = NULL;
MethodDesc::PFN_HasClassOrMethodInstantiation MethodDesc::s_pfnHasClassOrMethodInstantiation = NULL;
MethodDesc::PFN_ContainsGenericVariables MethodDesc::s_pfnContainsGenericVariables = NULL;
MethodDesc::PFN_GetWrappedMethodDesc MethodDesc::s_pfnGetWrappedMethodDesc = NULL;
MethodDesc::PFN_GetModule MethodDesc::s_pfnGetModule = NULL;
MethodDesc::PFN_GetLoaderModule MethodDesc::s_pfnGetLoaderModule = NULL;


LoadedMethodDescIterator::PFN_LoadedMethodDescIteratorConstructor LoadedMethodDescIterator::s_pfnConstructor = NULL;
LoadedMethodDescIterator::PFN_Start LoadedMethodDescIterator::s_pfnStart = NULL;
LoadedMethodDescIterator::PFN_Next LoadedMethodDescIterator::s_pfnNext = NULL;
LoadedMethodDescIterator::PFN_Current LoadedMethodDescIterator::s_pfnCurrent = NULL;

// detect the version of CLR
BOOL DetermineDotNetVersion(void)
{
	WCHAR wszPath[MAX_PATH] = {0};
	::GetModuleFileNameW( g_hClrModule, wszPath, MAX_PATH);
	CStringW strPath(wszPath);
	int nIndex = strPath.ReverseFind('\\');
	if( nIndex <= 0 )
		return FALSE;
	nIndex++;
	CStringW strFilename = strPath.Mid( nIndex, strPath.GetLength() - nIndex);
	if( strFilename.CompareNoCase(L"mscorwks.dll") == 0 )
	{
		g_tDotNetVersion = DotNetVersion_20;
		return TRUE;
	}

	if( strFilename.CompareNoCase(L"clr.dll") == 0 )
	{
		VS_FIXEDFILEINFO tVerInfo = {0};
		if ( CUtility::GetFileVersion( wszPath, &tVerInfo) &&
			 tVerInfo.dwSignature == 0xfeef04bd)
		{
			int nMajor = HIWORD(tVerInfo.dwFileVersionMS);
			int nMinor = LOWORD(tVerInfo.dwFileVersionMS);
			int nBuildMajor = HIWORD(tVerInfo.dwFileVersionLS);
			int nBuildMinor = LOWORD(tVerInfo.dwFileVersionLS);

			if( nMajor == 4 )
			{
				if(nMinor < 5)
					g_tDotNetVersion = DotNetVersion_40;
				else
					g_tDotNetVersion = DotNetVersion_45;
				return TRUE;
			}
		}
		return FALSE;
	}

	return FALSE;
}
