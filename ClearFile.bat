@echo off
set nowPath=%cd%
cd /
cd %nowPath%

::delete specify file(*.pdb,*.vshost.*)
for /r %nowPath% %%i in (*.pdb,*.vshost.*) do (del %%i)

::delete specify folder(obj,bin,.vs,.git,DebugC)
for /r %nowPath% %%i in (obj,bin,.vs,.git,DebugC) do (IF EXIST %%i RD /s /q %%i)

@ echo All_File_Remove_End!!!!!!
pause