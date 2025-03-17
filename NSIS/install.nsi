;-------------------------------------------------------------------------------
; Includes
!include "MUI2.nsh"
!include "LogicLib.nsh"
!include "WinVer.nsh"
!include "x64.nsh"

;-------------------------------------------------------------------------------
; Constants
!define PRODUCT_NAME "AutoPuTTY"
!define PRODUCT_DESCRIPTION "${PRODUCT_NAME}"
!define COPYRIGHT "Copyright © 2025 r4dius"
!define PRODUCT_VERSION "0.5.0.0"
!define SETUP_VERSION 0.5.0.0
!define AUTOPUTTY_VERSION "0.50"
!define PUTTY_VERSION "0.83"

;-------------------------------------------------------------------------------
; Attributes
Name "${PRODUCT_NAME}"
OutFile "AutoPuTTY installer.exe"
InstallDir "$PROGRAMFILES\${PRODUCT_NAME}"
InstallDirRegKey HKCU "Software\${PRODUCT_NAME}" ""
RequestExecutionLevel admin ; user|highest|admin

;-------------------------------------------------------------------------------
; Version Info
VIProductVersion "${PRODUCT_VERSION}"
VIAddVersionKey "ProductName" "${PRODUCT_NAME}"
VIAddVersionKey "ProductVersion" "${PRODUCT_VERSION}"
VIAddVersionKey "FileDescription" "${PRODUCT_DESCRIPTION}"
VIAddVersionKey "LegalCopyright" "${COPYRIGHT}"
VIAddVersionKey "FileVersion" "${SETUP_VERSION}"

;-------------------------------------------------------------------------------
; Modern UI Appearance
!define MUI_ICON "../Resources/autoputty.ico"
!define MUI_WELCOMEFINISHPAGE_BITMAP "left.bmp"
!define MUI_FINISHPAGE_NOAUTOCLOSE

;-------------------------------------------------------------------------------
; Installer Pages
!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_COMPONENTS
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH

;-------------------------------------------------------------------------------
; Uninstaller Pages
!insertmacro MUI_UNPAGE_WELCOME
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES
!insertmacro MUI_UNPAGE_FINISH

;-------------------------------------------------------------------------------
; Languages
!insertmacro MUI_LANGUAGE "English"

;-------------------------------------------------------------------------------
; Installer Sections
Section "autoputty ${AUTOPUTTY_VERSION}" autoputty
	SetOutPath $INSTDIR
	File "autoputty.exe"
	WriteUninstaller "$INSTDIR\uninstall.exe"

	; Write the installation path into the registry
	WriteRegStr HKLM "Software\${PRODUCT_NAME}" "Install_Dir" "$INSTDIR"

	; Write the uninstall keys for Windows
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}" "DisplayName" "${PRODUCT_NAME}"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}" "UninstallString" '"$INSTDIR\uninstall.exe"'
	WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}" "NoModify" 1
	WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}" "NoRepair" 1
SectionEnd

Section "putty ${PUTTY_VERSION}" putty
	SetOutPath $INSTDIR
	File "putty.exe"
SectionEnd

Section "Start Menu Shortcuts"
  CreateDirectory "$SMPROGRAMS\${PRODUCT_NAME}"
  CreateShortcut "$SMPROGRAMS\${PRODUCT_NAME}\Uninstall.lnk" "$INSTDIR\uninstall.exe"
  CreateShortcut "$SMPROGRAMS\${PRODUCT_NAME}\${PRODUCT_NAME}.lnk" "$INSTDIR\autoputty.exe"
SectionEnd

Function .onInit
	IntOp $0 ${SF_SELECTED} | ${SF_RO}
	SectionSetFlags ${autoputty} $0
	SectionSetFlags ${putty} $0
FunctionEnd

;-------------------------------------------------------------------------------
; Uninstaller Sections
Section "Uninstall"
	Delete "$SMPROGRAMS\${PRODUCT_NAME}\*.lnk"
	Delete "$INSTDIR\autoputty.exe"
	Delete "$INSTDIR\putty.exe"
	Delete "$INSTDIR\uninstall.exe"
	RMDir "$SMPROGRAMS\${PRODUCT_NAME}"
	RMDir "$INSTDIR"
	DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
	DeleteRegKey /ifempty HKCU "Software\${PRODUCT_NAME}"
SectionEnd