using System;
using System.Runtime.CompilerServices;
using System.IO;
using System.ComponentModel.Design;
using static System.Formats.Asn1.AsnWriter;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;
using System.Reflection.Emit;
using System.Security.Cryptography;

public class Level
{
    public int[,] level_layout;
    public int numerPoziomu;
    public int rozmiarX, rozmiarY;
    public Level (int n, int x, int y)
    {
        this.rozmiarY = y;
        this.numerPoziomu = n;
        this.rozmiarX = x;
        this.level_layout = new int[x,y];
    }
}
public class SaveManager
{
    int numberOfLevels;
    public SaveManager(int n) { numberOfLevels = n; }
    public Level[] listOfLevels;
    public void loadFiles ()
    {
        listOfLevels = new Level[numberOfLevels+1];
        for (int i = 1; i <= numberOfLevels; i++)
        {
            string s = "level" + i.ToString() + ".txt";
            string folderPath = AppDomain.CurrentDomain.BaseDirectory;
            string[] textLines = File.ReadAllLines(folderPath + s);
            listOfLevels[i] = new Level(Int32.Parse(textLines[0]), Int32.Parse(textLines[1]), Int32.Parse(textLines[2]));
            for (int j = 0; j < listOfLevels[i].rozmiarY; j++)
            {
                for (int k = 0; k< listOfLevels[i].rozmiarX; k++)
                {
                    listOfLevels[i].level_layout[k, j] = Int32.Parse(textLines[3 + k + ((j) * listOfLevels[i].rozmiarX)]);
                }
            }
        }
    }
    public Level newGame()
    {
        return listOfLevels[1];
    }
    public Level loadGame()
    {
        string s = "save.txt";
        string[] textLines = File.ReadAllLines("C:\\files\\" + s);
        listOfLevels[0] = new Level(Int32.Parse(textLines[0]), Int32.Parse(textLines[1]), Int32.Parse(textLines[2]));
        for (int j = 0; j < listOfLevels[0].rozmiarY; j++)
        {
            for (int k = 0; k < listOfLevels[0].rozmiarX; k++)
            {
                listOfLevels[0].level_layout[k, j] = Int32.Parse(textLines[3 + k + ((j) * listOfLevels[0].rozmiarX)]);
            }
        }
        return listOfLevels[0];
    }
    public bool isLastLevel(int n)
    {
        if (n == numberOfLevels) return true;
        else return false;
    }
    public Level nextLevel(int n)
    {
        return listOfLevels[n+1];
    }
    public void saveGame(Level a, int health, int score)
    {
        File.WriteAllText("C:\\files\\save.txt", String.Empty);
        
        string loadOut = a.numerPoziomu.ToString() + '\n' + a.rozmiarX.ToString() + '\n' + a.rozmiarY.ToString() + '\n';
        for (int j = 0; j < a.rozmiarY; j++)
        {
            for (int k = 0; k < a.rozmiarX; k++)
            {
                loadOut = loadOut + a.level_layout[k, j].ToString() + '\n';
            }
        }
        File.WriteAllText("C:\\files\\save.txt", loadOut);

        File.WriteAllText("C:\\files\\player.txt", String.Empty);
        File.WriteAllText("C:\\files\\player.txt", health.ToString() + '\n' + score.ToString());
    }
    public int loadPlayerHealth()
    {
        string s = "player.txt";
        string[] textLines = File.ReadAllLines("C:\\files\\" + s);
        return Int32.Parse(textLines[0]);
    }
    public int loadPlayerScore()
    {
        string s = "player.txt";
        string[] textLines = File.ReadAllLines("C:\\files\\" + s);
        return Int32.Parse(textLines[1]);
    }
} 
public class C
{
    public int x;
    public int y;
    public C(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public C(C c) { this.x = c.x; this.y = c.y; }
 }
public class MapEngine  
{
    private Level map;
    private Player player;
    private C expectedPosition;
    private C[] listOfMonsters;
    private int numberOfMonsters;
    private int movedTile;
    public void newLevel(Level a)
    {
        numberOfMonsters = 0;
        this.map = a;
        for ( int i=0; i<map.rozmiarX; i++)
        {
            for (int j=0; j<map.rozmiarY; j++)
            {
                if (map.level_layout[i, j] == 5) numberOfMonsters++; 
            }
        }
        if (numberOfMonsters > 0) 
        {
            listOfMonsters = new C[numberOfMonsters];
            int g = 0;
            for (int i = 0; i < map.rozmiarX; i++)
            {
                for (int j = 0; j < map.rozmiarY; j++)
                {
                    if (map.level_layout[i, j] == 5)
                    {
                        listOfMonsters[g] = new C(i, j);
                        g++;
                    }
                }
            }
        }
    }
    public Level getMap() { return map; }
    public void grabPlayer(Player player) { this.player = player; }
    public void printMap()
    {
        Console.WriteLine("Poziom:" + map.numerPoziomu.ToString());
        for (int i = 0; i < map.rozmiarY; i++)
        {
            for (int j = 0; j < map.rozmiarX; j++)
            {
                Console.OutputEncoding = System.Text.Encoding.GetEncoding(28591);
                switch (map.level_layout[j, i])
                {
                    case 0:
                        Console.Write(". ");
                        break;
                    case 1:
                        Console.Write((char)219 + " ");
                        break;
                    case 3:
                        Console.Write((char)150 + " ");
                        break;
                    case 4:
                        Console.Write((char)97 + " ");
                        break;
                    case 2:
                        Console.Write(("x "));
                        break;
                    case 5:
                        Console.Write(("# "));
                        break;
                    case 6:
                        Console.Write(("$ "));
                        break;
                    case 7:
                        Console.Write(("H "));
                        break;
                    case 8:
                        Console.Write(("I "));
                        break;
                    case 9:
                        Console.Write(("P "));
                        break;
                    default:
                        if (map.level_layout[j, i] > 199) Console.Write(("\\ "));
                        else if (map.level_layout[j, i] > 99) Console.Write(("D "));
                        else Console.Write(("k "));
                        break;
                }
                    
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
    public C searchPlayer()
    {
        for(int i = 0; i < map.rozmiarY; i++)
        {
            for (int j = 0; j < map.rozmiarX; j++)
            {
                if (map.level_layout[j, i] == 4) return new C(j, i);        
            }
        }
        return new C(0, 0);
    }
    public bool isMoveLegal(C position, char direction)
    {
        
        switch (direction)
        {
            case 'd':
                {
                    expectedPosition = new C(position.x, position.y+1);
                    
                    break;
                }
            case 'g':
                {
                    expectedPosition = new C(position.x, position.y -1 );
                    
                    break;
                }
            case 'l':
                {
                    expectedPosition = new C(position.x-1, position.y);
                    
                    break;
                }
            case 'p':
                {
                    expectedPosition = new C(position.x+1, position.y);
                   
                    break;
                }
        }
        if (expectedPosition.x < 0 || expectedPosition.y < 0 || expectedPosition.x >= map.rozmiarX || expectedPosition.y >= map.rozmiarY) return false;
        if (map.level_layout[expectedPosition.x, expectedPosition.y] > 99 && map.level_layout[expectedPosition.x, expectedPosition.y] <200) return player.doorCheck(map.level_layout[expectedPosition.x, expectedPosition.y], 0);
        if (map.level_layout[expectedPosition.x, expectedPosition.y] > 200)
        {
            for (int i = 0; i < map.rozmiarX; i++)
                for (int j = 0; j < map.rozmiarY; j++)
                    if (map.level_layout[i, j] == map.level_layout[expectedPosition.x, expectedPosition.y] - 100) map.level_layout[i, j] = 0;
            return false;
        }
        if (map.level_layout[expectedPosition.x, expectedPosition.y] != 1) return true;
        else return false;
        return true;
    }
    public bool isMoveLegalMonsterEdition(C position, char direction)
    {

        switch (direction)
        {
            case 'd':
                {
                    expectedPosition = new C(position.x, position.y + 1);

                    break;
                }
            case 'g':
                {
                    expectedPosition = new C(position.x, position.y - 1);

                    break;
                }
            case 'l':
                {
                    expectedPosition = new C(position.x - 1, position.y);

                    break;
                }
            case 'p':
                {
                    expectedPosition = new C(position.x + 1, position.y);

                    break;
                }
        }
        if (expectedPosition.x < 0 || expectedPosition.y < 0 || expectedPosition.x >= map.rozmiarX || expectedPosition.y >= map.rozmiarY) return false;
        if (map.level_layout[expectedPosition.x, expectedPosition.y] == 0) return true;
        else return false;
    }
    public bool isMonsterAttack (C Mposition, C Pposition)
    {
        int differencex = Mposition.x - Pposition.x;
        int differeney =  Mposition.y - Pposition.y;
        if (differencex==-1 && differeney == 0) return true;
        if (differencex == 1 && differeney == 0) return true;
        if (differencex == 0 && differeney == 1) return true;
        if (differencex == 0 && differeney == -1) return true;
        return false;
    }
    public void monsterMove ()
    {
        for (int i=0; i<numberOfMonsters; i++)
        {
            if (isMonsterAttack(listOfMonsters[i],player.getPosition()) == true) player.damage();
            else
            {
                
                bool legality = false;
               
                C expectedTile = new C(-1,-1);
                while (legality == false)
                {
                    Random rnd = new Random();
                    int direction = rnd.Next(1, 5);
                    switch (direction)
                    {
                        case 1: legality = isMoveLegalMonsterEdition(listOfMonsters[i], 'g'); expectedTile = new C(listOfMonsters[i].x, listOfMonsters[i].y-1); break;
                        case 2: legality = isMoveLegalMonsterEdition(listOfMonsters[i], 'd'); expectedTile = new C(listOfMonsters[i].x, listOfMonsters[i].y+1); break;
                        case 3: legality = isMoveLegalMonsterEdition(listOfMonsters[i], 'p'); expectedTile = new C(listOfMonsters[i].x+1, listOfMonsters[i].y); break;
                        case 4: legality = isMoveLegalMonsterEdition(listOfMonsters[i], 'l'); expectedTile = new C(listOfMonsters[i].x-1, listOfMonsters[i].y); break;
                    }
                }
                map.level_layout[expectedTile.x, expectedTile.y] = 5;
                map.level_layout[listOfMonsters[i].x, listOfMonsters[i].y] = 0;
                listOfMonsters[i] = new C(expectedTile.x, expectedTile.y);
                

            }
        }
    }
    public void playerMove(C position, char direction) 
    {
        switch (direction)
        {
            case 'd':
                {
                    expectedPosition = new C(position.x, position.y + 1);
                    movedTile = map.level_layout[expectedPosition.x, expectedPosition.y];
                    break;
                }
            case 'g':
                {
                    expectedPosition = new C(position.x, position.y - 1);
                    movedTile = map.level_layout[expectedPosition.x, expectedPosition.y];
                    break;
                }
            case 'l':
                {
                    expectedPosition = new C(position.x - 1, position.y);
                    movedTile = map.level_layout[expectedPosition.x, expectedPosition.y];
                    break;
                }
            case 'p':
                {
                    expectedPosition = new C(position.x + 1, position.y);
                    movedTile = map.level_layout[expectedPosition.x, expectedPosition.y];
                    break;
                }
        }
        map.level_layout[expectedPosition.x, expectedPosition.y] = 4;
        map.level_layout[position.x, position.y] = 0;
    }
    public int moveTile ()
    {
        return movedTile;
    }

}
public class Player
{
    public C position;
    private int health;
    private int score;
    private bool protection;
    private bool power;
    public int keyHolder = 0;
    public Player (int health, int score)
    {
        this.health = health;
        this.score = score;
    }
    public int getHealth() {  return health; }
    public void heal() { this.health = 5; }
    public int getScore() { return score; }
    public int getKeys() {  return keyHolder; }
    public C getPosition() { return position; }
    public void protectionPotionOn() { protection = true; }
    public void powerPotionOn() { power = true; }
    public void PotionOff() { protection = false; power = false; }
    public bool isProtected() { return protection; }
    public bool isPowerful() { return power; }
    public void printStatus() { if (protection == true) { Console.WriteLine("Gracz jest nieśmiertelny!"); } if (power == true) Console.WriteLine("Gracz jest potężny!"); }
    public bool isAlive ()
    {
        if (this.health > 0) return true;
        else return false ;
    }
    public void printStats ()
    {
        Console.WriteLine("Zdrowie: " +  this.health + " Wynik: " + this.score);
        Console.WriteLine("Posiadane klucze:" + this.keyHolder);
    }
    public void damage() { health--; }
    public void treasure() { score++; }
    
    public void collectKey(int a)
    {
        keyHolder *= 100;
        keyHolder += a;
    }
    public bool doorCheck( int a, int g)
    {
        
        a = a % 100;
        g = getKeys();
        while (g > 0)
        {
            if (g % 100 == a) return true;
            else
            {
                g = g - (g % 100);
                g /= 100;
            }
        }
        return false;
    }
}
public class Controller
{
    private SaveManager manager;
    private MapEngine engine;
    private Player player;
   
    public Controller (SaveManager manager, MapEngine engine, Player player)
    {
        this.manager = manager;
        this.engine = engine;
        this.player = player;
    }
    public void startGame()
    {
        manager.loadFiles();
         
        while (true)
        {
            Console.WriteLine("Nowa Gra czy Wczytaj Grę?");
            string odpowiedz = Console.ReadLine();
            if (odpowiedz == "Nowa")
            {
                engine.newLevel(manager.newGame());
                player = new Player(5, 0);
                player.position = new C(engine.searchPlayer());
                engine.grabPlayer(player);
                break;
            }
            else if (odpowiedz == "Wczytaj")
            {
                engine.newLevel(manager.loadGame());
                player = new Player(manager.loadPlayerHealth(), manager.loadPlayerScore());
                player.position = new C(engine.searchPlayer());
                engine.grabPlayer(player);
                break;
            }
        }
    }
    public void gamingTime()
    {
        int guard =0;
        int potionTimer = 0;
        while (player.isAlive())
        {
            if (potionTimer > 0) potionTimer--;
            if (potionTimer == 0)
            {
                player.PotionOff();
            }
            engine.printMap();
            player.printStats();
            player.printStatus();
            string input = Console.ReadLine();
            switch (input)
            {
                case "Zapisz": manager.saveGame(engine.getMap(), player.getHealth(), player.getScore()); break;
                case "w":
                    {
                        //sprawdz czy ruch jest legalny
                        if (engine.isMoveLegal(player.getPosition(), 'g'))
                        {
                            engine.playerMove(player.getPosition(), 'g');
                            player.position = new C(player.getPosition().x, player.getPosition().y - 1);
                            guard = 1;
                        }
                        else guard = 0;
                        break;
                    }
                case "s":
                    {
                        //sprawdz czy ruch jest legalny
                        if (engine.isMoveLegal(player.getPosition(), 'd'))
                        {
                            engine.playerMove(player.getPosition(), 'd');
                            player.position = new C(player.getPosition().x, player.getPosition().y + 1);
                            guard = 1;
                        }
                        else guard = 0;
                        break;
                    }
                case "a":
                    {
                        //sprawdz czy ruch jest legalny
                        if (engine.isMoveLegal(player.getPosition(), 'l'))
                        {
                            engine.playerMove(player.getPosition(), 'l');
                            player.position = new C(player.getPosition().x - 1, player.getPosition().y);
                            guard = 1;
                        }
                        else guard = 0;
                        break;
                    }
                case "d":
                    {
                        //sprawdz czy ruch jest legalny
                        if (engine.isMoveLegal(player.getPosition(), 'p'))
                        {
                            engine.playerMove(player.getPosition(), 'p');
                            player.position = new C(player.getPosition().x + 1, player.getPosition().y);
                            guard = 1;
                        }
                        else guard = 0;
                        break;
                    }
                default: { guard = 0; break; }
            }
            if (engine.moveTile() == 3)
            {
                if (manager.isLastLevel(engine.getMap().numerPoziomu) == false)
                {
                    engine.newLevel(manager.nextLevel(engine.getMap().numerPoziomu));
                    player.position = engine.searchPlayer();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Wielki Sukces! Wynik końcowy: " + player.getScore().ToString());
                    break;
                }

            }
            if (engine.moveTile() == 5)
            {
                if (guard==1 && player.isPowerful() == false)
                {
                    player.damage();
                    player.damage();
                    player.damage();
                    player.damage();
                    player.damage();
                }
            }
            if (engine.moveTile() == 2)
            {
                if (guard == 1 && player.isProtected() == false) player.damage();
            }
            if (engine.moveTile() == 6)
            {
                if (guard ==1) player.treasure();
            }
            if (engine.moveTile() == 7)
            {
                if (guard == 1) player.heal();
            }
            if (engine.moveTile() == 8)
            {
                if (guard == 1)
                {
                    player.protectionPotionOn();
                    potionTimer += 10;
                }
            }
            if (engine.moveTile() == 9)
            {
                if (guard == 1)
                {
                    player.powerPotionOn();
                    potionTimer += 10;
                }
            }
            if (engine.moveTile() < 100 && engine.moveTile() > 9)
            {
                if (guard ==1)
                {
                    player.collectKey(engine.moveTile());
                }
            }
            engine.monsterMove();
            Console.Clear();
        }
      
        if (player.isAlive() == false)
        {
            Console.Clear();
            Console.WriteLine("Koniec gry! Wynik końcowy: " + player.getScore().ToString());
        }

    }
}
class Man
{
    public static void Main()
    {
        SaveManager manager = new SaveManager(3);
        MapEngine engine = new MapEngine();
        Player player = new Player(-1, -1);
        Controller controller = new Controller(manager, engine, player);
        while (true)
        {


            controller.startGame();
            controller.gamingTime();
        }
    }
}
    