#Gast-Zugang erstellen
Einleitung -> tbd

####Wichtiges
Um das App und all seine Feature richtig Nutzen zu können muss man Zugang zum LAN von APG haben.
Ansonsten kann man das App und seine Features nicht nutzen. 
#####
Auch ist es von Vorteil einen Drucker zu ahben,
denn ohne Drucker muss man sich den Benutzer und sein Passwort von selber aufschreiben oder es sich merken.
Das Programm Druckt automatisch über den Durcker, der als Standard Drucker für das Gerät, aufdem das Programm installiert ist.
#####
Auch braucht man, um sich bei diesem App einzuloggen einen Benutzer und ein Passwort.
Dieser Benutzername und Passwort sollten Sie erhalten haben, wenn nicht schauen Sie im 
APG Wiki nach, dort sollte eine Anleitung zum erstellen eines Gast-Benutzer vorhanden sein.
##Installation
####Über Cluebiz
 - Cluebiz?
####Über GitHub-Releases
 Eine andere Variante ist es Unter "Releases" den neusten Release vom Gast-Zugang App herunterzuladen,
 im Downloads Ordner die Datei "Publish.zip" zu entpacken und im Ordner die Datei "setup.exe" auszuführen.
####Über GitHub Code
 Einea andere Variante ist es den kompletten Code herunter zu laden und das File "Publish.zip"
 im Ordner "APGSGA-GPP_App" zu entpacken und dort die datei "setup.exe" auf Ihrem Windows PC
 laufen zu lassen, indem Sie auf die Datei Doppelclicken.
##Dokumentation
 Die Dokumentation ist eine Übersicht der Files und Funktionen, welche ich in diesem Projekt entwickelt habe.
 Ich empfehle, jedem, der wissen will, wie das Programm Funktioniert und eine Grundlegende Ahnung hat von Programmieren
 diese Dokumentation zu lesen. Es ist kein muss diese Dokumentation zu lesen, wenn man das know how nicht hat oder keine lust dazu.
###Grundlegendes
 Dieses App wude mithilfe von Visual Studio 2017, als eine Windows-Forms Applikation entwickelt und besitzt 3 Forms und 3 andere Classes.
#####Die Forms: 
  1. Form1 - Das eigentliche Hauptprogramm, also das Main-GUI der Applikation
  2. login - Das, wie der Name schon sagt, Login-GUI
  3. Form2 - Das GUI, für die Daten-Anzeige, also dieses GUI wird im moment (Stand 24.07.2020) nicht benutzt dient aber, um die erstellten Benutzer anzeigen zu können.
#####Die Klassen:
1. Program - Der Eintrittspunkt der Applikation, also das haupt Script.
2. User - Eine Klasse zum Aufteilen von Daten, also eine Klasse, die nur Benutzer Daten erstellt und Speichert.
3. LoginData - Ähnlich, wie User einfach diesmal für die Login-Informationen
###Scripts
####Program
Das Script Program.cs ist der Eintritts Punkt der Applikation. Um mit Windows-Forms arbeiten zu können, müssen die Forms in einen Loop eingebettet werden.
Dieses Script öffnet als erstes natürlich den `loginLoop`, also öffnet es in einem Thread die Form `login.cs`:
```     
        static void Main(string[] args)
        {
            //Make the Login-Loop
            Thread thread = new Thread(loginLoop);
            thread.Start();
        }
```

######
Wenn der Benutzer dann den Benutzernamen und das Passwort eingegeben hat sendet die Form `login.cs` den eingegebenen Benutzernamen und das Passwort zurück ans `Program.cs`.
Dort wird mithilfe der Daten von der Klasse `LoginData`, welche alle Benutzernamen und Passwörter in einem `Dictionary` festhaltet. 
Wenn dann der Login Validiert ist startet die Appliaktion die Form `Form1`, als Hauptfenster der Applikation. 


######
