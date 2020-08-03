<h1>Gast-Zugang erstellen</h1>

<p>Einleitung -> tbd</p>

<h4>Wichtiges</h4>

<p>Um das App und all seine Feature richtig Nutzen zu können muss man Zugang zum LAN von APG haben.
Ansonsten kann man das App und seine Features nicht nutzen. </p>

<p>Auch ist es von Vorteil einen Drucker zu ahben,
denn ohne Drucker muss man sich den Benutzer und sein Passwort von selber aufschreiben oder es sich merken.
Das Programm Druckt automatisch über den Durcker, der als Standard Drucker für das Gerät, aufdem das Programm installiert ist.</p>

<p>Auch braucht man, um sich bei diesem App einzuloggen einen Benutzer und ein Passwort.
Dieser Benutzername und Passwort sollten Sie erhalten haben, wenn nicht schauen Sie im 
APG Wiki nach, dort sollte eine Anleitung zum erstellen eines Gast-Benutzer vorhanden sein.</p>

<h2>Installation</h2>

<h4>Über Cluebiz</h4>

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

<h2>Anleitung</h2>
<ol>

<li>

Starten Sie die APP: <code>APGSGA-GPP_APP</code>

<div style="text-align:left"><img src=".\bilder\APGSGA_APP.jpg" alt="APGSGA-GPP_APP"/></div>

</li>

<li>

Melden Sie sich mit Ihren Anmelde Daten an.

<div style="text-align:left"><img src=".\bilder\login.png" alt="Form: login.cs"/></div>

</li>

<li>

Geben Sie den Benutzernamen und Passwort ein, falls Sie etwas an dem Vorgeschlagenem Benutzer ändern wollen.

<div style="text-align:left"><img src=".\bilder\form1_Benutzer.png" alt="Form: Form1.cs Benutzer markiert"/></div>

</li>

<li>

Wählen Sie das Datum aus, von wann bis wann der Benutzer aktiv sein soll.

<div style="text-align:left"><img src=".\bilder\form1_Date.png" alt="Form: Form1.cs Datum markiert"/></div>

</li>

<li>

Drücken Sie auf Create and Print.

<div style="text-align:left"><img src=".\bilder\form1_Create.png" alt="Form: Form1.cs Create markiert"/></div>

</li>

<li>Das Dokument mit den Anmelde Daten wird jetzt in Ihrem Standard-Drucker gedruckt.</li>

<li>Geben Sie das Dokument Ihrem Gast.</li>

</ol>

<h2>Dokumentation</h2>

<p>Die Dokumentation ist eine Übersicht der Files und Funktionen, welche ich in diesem Projekt entwickelt habe.
 Ich empfehle, jedem, der wissen will, wie das Programm Funktioniert und eine Grundlegende Ahnung hat von Programmieren
 diese Dokumentation zu lesen. Es ist kein muss diese Dokumentation zu lesen, wenn man das know how nicht hat oder keine lust dazu.</p>

<h3>Grundlegendes</h3>

<p>Dieses App wude mithilfe von Visual Studio 2017, als eine Windows-Forms Applikation entwickelt und besitzt 3 Forms und 3 andere Classes.</p>

<h4>Die Forms:</h4>

<ol>
<li>Form1 - Das eigentliche Hauptprogramm, also das Main-GUI der Applikation</li>

<li>login - Das, wie der Name schon sagt, Login-GUI</li>

<li>Form2 - Das GUI, für die Daten-Anzeige, also dieses GUI wird im moment (Stand 24.07.2020) nicht benutzt dient aber, um die erstellten Benutzer anzeigen zu können.</li>
</ol>

<h4>Die Klassen:</h4>

<ol>
<li>Program - Der Eintrittspunkt der Applikation, also das haupt Script.</li>

<li>User - Eine Klasse zum Aufteilen von Daten, also eine Klasse, die nur Benutzer Daten erstellt und Speichert.</li>

<li>LoginData - Ähnlich, wie User einfach diesmal für die Login-Informationen</li>
</ol>

<h3>Scripts</h3>

<h4>Allgemeiner Ablauf</h4>

<p>Das Script Program.cs ist der Eintritts Punkt der Applikation. Um mit Windows-Forms arbeiten zu können, müssen die Forms in einen Loop eingebettet werden.
Dieses Script öffnet als erstes natürlich den <code>loginLoop</code>, also öffnet es in einem Thread die Form <code>login.cs</code>:</p>

<pre><code>        static void Main(string[] args)
        {
            //Make the Login-Loop
            Thread thread = new Thread(loginLoop);
            thread.Start();
        }
</code></pre>

<div style="text-align:center"><img src=".\bilder\login.png" alt="Form: login.cs"/></div>

<p>Wenn der Benutzer dann den Benutzernamen und das Passwort eingegeben hat sendet die Form <code>login.cs</code> den eingegebenen Benutzernamen und das Passwort zurück ans <code>Program.cs</code>.
Dort wird mithilfe der Daten von der Klasse <code>LoginData</code>, welche alle Benutzernamen und Passwörter in einem <code>Dictionary</code> festhaltet. 
Wenn dann der Login Validiert ist startet die Appliaktion die Form <code>Form1</code>, als Hauptfenster der Applikation. </p>

<div style="text-align:center"><img src=".\bilder\form1.png" alt="Form: form1.cs" /></div>

<p>Wenn die Form dann gstartet ist, was immer etwa 2 Sekunden braucht generiert das Script <code>Program.cs</code> schon den ersten Benutzer, wenn ich von
Benutzer rede, meine ich immer einen Benutzernamen und ein Passwort, dass die Klasse <code>User.cs</code> bereitstellt. Unterhalb des Users sieht man auch
eine Zeitspanne, also zwei <code>DatePicker</code>, eines Von und das andere Bis.</p>

<p>Wenn man jetzt einen Benutzer erstellen möchte Drückt man auf den Button Create and Print:</p>

<div style="text-align:left"><img src=".\bilder\form1_Create.png" alt="Form: Form1.cs Create markiert"/></div>

<p>Dann werden 2 Threads generiert, auf  dem ersten wird ein Querry-String generiert, der z.B. so aussehen kann: <code>local-userdb-guest add username "TestUser" password "pjox7856" start-time 07/08/2020 16:29 expiry time 07/08/2020 18:00</code>
mit diesem Querry-String wird ein neuer Benutzer generiert, der <code>TestUser</code> heisst, mit dem passwort <code>pjox7856</code>, der am <code>07/08/2020 16:29</code>
gültig wird und am <code>07/08/2020 18:00</code> wieder ungültig ist. Auf dem 2. Thread wird dann gedruckt, also mithilfe der Microsoft.Office.Interop.Word
Library wird ein Word generiert, dass so aussieht:</p>

<div style="text-align:left"><img src=".\bilder\Word.png" alt="Ouput Word"/></div>

<h4>Program.cs</h4>

<p>Die Klasse <code>Program.cs</code> besteht grob gesagt aus 6 Punkten:</p>

<ol>

<li>Main - Der Eintrittspunkt, den wir oben im Code beispiel schon gesehen haben.</li>

<li>Die Loops - Alle Loops, die Benötigt werden, also einen Pro Form: <code>MainLoop()</code>, <code>showLoop()</code> und <code>loginLoop()</code>.</li>

<li>Querry - Dieser Funktion kann man einen String mitgeben, welchen Sie dann mit den Login Informationen, die man beim <code>loginLoop()</code> eingeben musste, auf dem Server ausführt.</li>

<li>
    Login Validation - Das sind zwei Funktionen, die Verantwortlich dafür sind, dass man sich einloggen kann:

<ul>
<li>Als erstes einen String, der dir den MD5-Hash eines Strings zurück gibt. Diese Funktion wird für die Passwort Validierung benötigt.</li>
<li>Als zweites eine Funktion, die mithilfe der <code>LoginData.cs</code> Klasse die Login Informationen überprüft und die Initialisierung des <code>MainLoops</code> beginnt.</li>
</ul>

</li>

<li>Dann noch eine Funktion, die den <code>MainLoop</code> Initialisiert. Also Sie nimmt aus dem Computernamen den Standort und setzt so eine Variable, die zum herleiten des benutzernamens verwendet wird.
Dann Initialisiert Sie, dass ein Benutzer erstellt wird.</li>

<li>Als letztes gibt es noch zwei Funktionen, die noch keine Wirkung haben, da sie nur Ansätze sind und nicht verwendet werden.</li>

</ol>

<h4>User.cs</h4>

<p><code>User.cs</code> ist eine Klasse, um Daten zu genereieren, die man für ein Login braucht, also einen Benutzernamen und ein Passwort.
Diese Klasse macht wirklich nicht mehr, als diese beiden Dinge zu speichern und zu generieren.</p>

<ol>

<li>Variablen - einen <code>public</code> username und ein <code>public</code> password</li>

<li>Generation:
    <ul>
        <li><code>CreatePW()</code> - Ein String, der aus der variable <code>const string chars = ""</code> random eines der Zeichen, im String auswählt und dann mit der länge einen neuen String generiert, der dann aus den Random ausgewählten Zeichen besteht.</li>
        <li><code>CreateUsername()</code> - Ein String, der aus dem Datum, welches im <code>DatePicker</code> steht und der Standort Bezeichnung des Rechners einen Benutzernamen generiert.</li>
    </ul>
</li>

</ol>

<h4>LoginData.cs</h4>

<p><code>LoginData.cs</code> ist eine Klasse, wie <code>User.cs</code>, die nur für die Daten speicherung und genereation verantwortlich ist, um genau zu sein, für die Login-Daten.</p>

<ol>

<li>Variablen - einen <code>public public Dictionary&lt;string, string></code> dic_login, also ein Dictionary mit dem Key als Benutzernamen und dem Value als Passwort.</li>

<li>Generation:

<ul>
    <li><code>CreateValidation()</code> - ein <code>Dictionary&lt;string, string></code>, der  die Benutzernamen und MD5 gehashte passwörter speichert.</li>
</ul>

</li>

</ol>