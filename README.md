<h1>Gast-Zugang erstellen</h1>

<p>Einleitung -> tbd</p>

<h4>Wichtiges</h4>

<p>Um das App und all seine Feature richtig Nutzen zu können muss man Zugang zum LAN von APG haben.
Ansonsten kann man das App und seine Features nicht nutzen. </p>

<h4></h4>

<p>Auch ist es von Vorteil einen Drucker zu ahben,
denn ohne Drucker muss man sich den Benutzer und sein Passwort von selber aufschreiben oder es sich merken.
Das Programm Druckt automatisch über den Durcker, der als Standard Drucker für das Gerät, aufdem das Programm installiert ist.</p>

<h4></h4>

<p>Auch braucht man, um sich bei diesem App einzuloggen einen Benutzer und ein Passwort.
Dieser Benutzername und Passwort sollten Sie erhalten haben, wenn nicht schauen Sie im 
APG Wiki nach, dort sollte eine Anleitung zum erstellen eines Gast-Benutzer vorhanden sein.</p>

<h2 id="installation">Installation</h2>

<h4 id="bercluebiz">Über Cluebiz</h4>

<ul>
<li>Cluebiz?</li>
</ul>

<h4>Über GitHub-Releases</h4>

<p>Eine andere Variante ist es Unter "Releases" den neusten Release vom Gast-Zugang App herunterzuladen,
 im Downloads Ordner die Datei "Publish.zip" zu entpacken und im Ordner die Datei "setup.exe" auszuführen.</p>

<h4>Über GitHub Code</h4>

<p>Einea andere Variante ist es den kompletten Code herunter zu laden und das File "Publish.zip"
 im Ordner "APGSGA-GPP_App" zu entpacken und dort die datei "setup.exe" auf Ihrem Windows PC
 laufen zu lassen, indem Sie auf die Datei Doppelclicken.</p>

<h2 id="dokumentation">Dokumentation</h2>

<p>Die Dokumentation ist eine Übersicht der Files und Funktionen, welche ich in diesem Projekt entwickelt habe.
 Ich empfehle, jedem, der wissen will, wie das Programm Funktioniert und eine Grundlegende Ahnung hat von Programmieren
 diese Dokumentation zu lesen. Es ist kein muss diese Dokumentation zu lesen, wenn man das know how nicht hat oder keine lust dazu.</p>

<h3 id="grundlegendes">Grundlegendes</h3>

<p>Dieses App wude mithilfe von Visual Studio 2017, als eine Windows-Forms Applikation entwickelt und besitzt 3 Forms und 3 andere Classes.</p>

<h5 id="dieforms">Die Forms:</h5>

<ol>
<li>Form1 - Das eigentliche Hauptprogramm, also das Main-GUI der Applikation</li>

<li>login - Das, wie der Name schon sagt, Login-GUI</li>

<li>Form2 - Das GUI, für die Daten-Anzeige, also dieses GUI wird im moment (Stand 24.07.2020) nicht benutzt dient aber, um die erstellten Benutzer anzeigen zu können.</li>
</ol>

<h5 id="dieklassen">Die Klassen:</h5>

<ol>
<li>Program - Der Eintrittspunkt der Applikation, also das haupt Script.</li>

<li>User - Eine Klasse zum Aufteilen von Daten, also eine Klasse, die nur Benutzer Daten erstellt und Speichert.</li>

<li>LoginData - Ähnlich, wie User einfach diesmal für die Login-Informationen</li>
</ol>

<h3>Scripts</h3>

<h4>Program</h4>

<p>Das Script Program.cs ist der Eintritts Punkt der Applikation. Um mit Windows-Forms arbeiten zu können, müssen die Forms in einen Loop eingebettet werden.
Dieses Script öffnet als erstes natürlich den <code>loginLoop</code>, also öffnet es in einem Thread die Form <code>login.cs</code>:</p>

<pre><code class="language-c#">        static void Main(string[] args)
        {
            //Make the Login-Loop
            Thread thread = new Thread(loginLoop);
            thread.Start();
        }
</code></pre>

<h5></h5>

<p>Wenn der Benutzer dann den Benutzernamen und das Passwort eingegeben hat sendet die Form <code>login.cs</code> den eingegebenen Benutzernamen und das Passwort zurück ans <code>Program.cs</code>.
Dort wird mithilfe der Daten von der Klasse <code>LoginData</code>, welche alle Benutzernamen und Passwörter in einem <code>Dictionary</code> festhaltet. 
Wenn dann der Login Validiert ist startet die Appliaktion die Form <code>Form1</code>, als Hauptfenster der Applikation. </p>

<h5 id="-3"></h5>

<p></p>
