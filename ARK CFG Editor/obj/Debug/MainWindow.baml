   M S B A M L   `   `   `  ????I  EARK CFG Editor, Version=1.4.1.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ARK_CFG_EditorARK_CFG_Editor  5   	       ARK_CFG_Editor.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:ARK_CFG_Editor     5   	     ? 
ResizeMode$  NoResize=?5   	    ? Title$ ARK CFG Editor??5	   	   $	??300??6    $	??490??6-   .??5
      ? .+?5   
   ?? -   5         ??Name$ LoadBtn??5      $??Запустить!??6"   $
??Left=?67   $??
135,10,0,0q?6R   $	??Top=?6f   $??83??6~   $??25??6?   6?   ?? 5   
   -   5         ??Name$ KeyCode??5      $
??Left=?6#   $??25??6>   $??	10,10,0,0q?6J    ?? TextWrapping$
 Wrap=?6]   $?? ??6q   $	??Top=?6y   $	??120??6?    ?? TextAlignment$ Center=?6?   ?? 5   
   -   5         ??Name$ 	CBShadows??5      $7??1Выключить тени и освещение??6\   $
??Left=?6?   $??	10,40,0,0q?6?   $	??Top=?6?   ?? 5   
   -   5        $ BuildBtn??5      $??Установить??6#   $
??Left=?68   $??
10,210,0,0q?6S   $	??Top=?6g   $	??217??6   6?   ?? 5   
   -   5        $ ConnectBug2??5      $??Подключиться??6&   $
??Left=?6=   $??249,40,-217,0q?6X   $	??Top=?6o   $	??100??6?   6?   ?? 5   
   -   5        $ ConnectBug3??5      $??Помощь??6&   $
??Left=?67   $??366,40,-217,0q?6R   $	??Top=?6i   $	??100??6?   6?   ?? 5   
   -   5        $ 	CBEffects??5      $7??1Выключить пиво, воду, тек (!)??6\   $
??Left=?6?   $??	10,55,0,0q?6?   $	??Top=?6?   ?? 5   
   -   5        $ 
CBEmitters??5      $8??2Выключить эффекты огнемёта??6_   $
??Left=?6?   $??	10,70,0,0q?6?   $	??Top=?6?   ?? 5   
   -	   5        $ 
CBFogHEAVY??5      $<??6Выключить туман (Тяж. метод) (!)??6_   $
??Left=?6?   $??	10,85,0,0q?6?   $	??Top=?6?   ?? 5   
   -
   5        $ 
CBFogLIGHT??5      $<??6Выключить туман (Лёг. метод) (!)??6_   $
??Left=?6?   $??
10,100,0,0q?6?   $	??Top=?6?   ?? 5   
   -   5        $ 	CBFoliage??5      $A??;Выключить листья, траву, кусты (!)??6\   $
??Left=?6?   $??
10,116,0,0q?6?   $	??Top=?6?   ?? 5   
   -   5        $ CBWaterSurface??5      $<??6Выключить поверхность воды (!)??6k   $
??Left=?6?   $??
10,131,0,0q?6?   $	??Top=?6?   ?? 5   
   -   5        $ CBOptimization??5      $3??-Оптимизировать картинку??6k   $
??Left=?6?   $??
10,146,0,0q?6?   $	??Top=?6?   ?? 5   
   -   5        $ CBFPSCap??5      $4??.Снять ограничение кадров??6Y   $
??Left=?6|   $??
10,161,0,0q?6?   $	??Top=?6?   ?? 5   
   -   5        $ CBLimitSettings??5      $<??6Ограничить настройки графики??6n   $
??Left=?6?   $??
10,176,0,0q?6?   $	??Top=?6?   ?? 5   
   -   5        $ CBOFFWarnings??5      $???9Откл. предупреждения программы??6h   $
??Left=?6?   $??
10,191,0,0q?6?   $	??Top=?6?   $	??217??6?   ?? 5   
   -   5        $
 Path??5      $
??Left=?6    $??25??6;   $??	10,10,0,0q?6G   $
 Wrap=?6Z   $?? ??6n   $	??Top=?6v   $	??217??6?   $ Center=?6?   ?? 5   
   -   5        $ 
ConnectBug??5      $
??Left=?6&   $??25??6A   $??249,10,-217,0q?6M   $
 Wrap=?6d   $?? ??6x   $	??Top=?6?   $	??217??6?   $ Center=?6?   ?? 5   
   -   5        $ CopyBtn??5      $??Скопировать??6"   $
??Left=?68   $??
10,235,0,0q?6S   $	??Top=?6g   $	??217??6   6?   ?? 5   
   -   5        $ NewPath??5      $8??2Изменить путь конфигурации??6"   $
??Left=?6G   $??	10,10,0,0q?6b   $	??Top=?6u   $	??217??6?   $??25??6?   ?? 5   
   -   5         ??Name$ Presets??5      $
??Left=?6$   $??
249,65,0,0q?6?   $	??Top=?6S   $	??217??6k   5       ?? 5!   
   -   5        $ 	CreateBtn??5!      $!??Создать пресет??6$   $
??Left=?6=   $??
249,92,0,0q?6X   $	??Top=?6l   $	??100??6?   6?   ?? 5"   
   -   5        $ DelBtn??5"      $!??Удалить пресет??6!   $
??Left=?6:   $??
366,92,0,0q?6U   $	??Top=?6i   $	??100??6?   6?   ?? 5#   
   -   5        $ 
InstallBtn??5#      $'??!Установить пресет??6%   $
??Left=?6A   $??249,117,0,0q?6\   $	??Top=?6q   $	??217??6?   6?   ?? 5$   
   -   5        $ UnlockMinimaps??5$      $5??/Разблокировать миникарты??6)   $
??Left=?6L   $??249,235,0,0q?6g   $	??Top=?6|   $	??217??6?   6?   5%      5&      