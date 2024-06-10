using System;
using System.ComponentModel;

public abstract class Obiekt
{
    int x, y;
    public virtual int getNumberTile() { return 0; }
    public Obiekt (int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public int test() { return 1; }
    
}
abstract class cierpienia : Obiekt
{
    int damage;
    public cierpienia(int damage,int x, int y) : base(x, y)
    {
        this.damage = damage;
    }
}
abstract class Mechanisms : Obiekt
{
    int tag;
    public Mechanisms(int tag,int x, int y) : base (x,y)
    {
        this.tag = tag;
    }
    public int getTag()
    {
        return tag;
    }
   
}
class Wall : Obiekt
{
   
    public override int getNumberTile()
    {
        return 1;   
    }
    public Wall(int x, int y) : base(x, y) { }
    
}
class Coin : Obiekt
{

    public int value;
    public Coin(int value, int x, int y) : base( x, y)
    {
        this.value = value;
    }
    public override int getNumberTile()
    {
        return 6;
    }
}
class Door : Mechanisms
    {
       
        public Door(int tag, int x, int y) : base(tag, x, y) { }
    public override int getNumberTile()
    {
        return base.getTag();
    }
}
class Key : Mechanisms
    {
        
        public Key(int tag, int x, int y) : base( tag, x, y)
        {
        }
    public override int getNumberTile()
    {
        return base.getTag();
    }
}
class Lever : Mechanisms
    {
        int tag;
        public Lever(int tag, int x, int y) : base(tag, x, y)
        {
            this.tag = tag;
        }
    public override int getNumberTile()
    {
        return base.getTag();
    }
}
class HPotion : Obiekt
    {
        
        string potionType;
        public HPotion(string type, int x, int y) : base( x, y)
        {
            this.potionType = type;
        }
    public override int getNumberTile()
    {
        return 7;
    }
}
    class IPotion : Obiekt
    {
     
        string potionType;
        public IPotion(string type, int x, int y) : base( x, y)
        {
            this.potionType = type;
        }
    public override int getNumberTile()
    {
        return 8;
    }
}
    class SPotion : Obiekt
    {
        string potionType;
        public SPotion(string type, int x, int y) : base( x, y)
        {
            this.potionType = type;
        }
    public override int getNumberTile()
    {
        return 9;
    }
}
    class Exit : Obiekt
    {
        public Exit(int x, int y) : base( x, y)
        {
        }
    public override int getNumberTile()
    {
        return 3;
    }
}
    class Monster : cierpienia
    {
        int damage;
        public Monster(int Bol, int x, int y) : base(Bol,x, y)
        {
        }
    public override int getNumberTile()
    {
        return 5;
    }
}
    class Trap : cierpienia
    {
        public Trap(int pulapkaBol, int x, int y) : base(pulapkaBol, x, y)
        {

        }
    public override int getNumberTile()
    {
        return 2;
    }
}
    class PlayerMarker : Obiekt
    {
        public PlayerMarker(int x, int y) : base(x, y) { }
    public override int getNumberTile()
    {
        return 4;
    }
}
class Empty : Obiekt
{
    public Empty(int x, int y) : base( x, y) { }
    public override int getNumberTile()
    {
        return 0;
    }
}
    public class Level
    {
        Obiekt[,] level_layout;
        public int numerPoziomu;
        public int rozmiarX, rozmiarY;
        public Level(int n, int x, int y)
        {
            this.rozmiarY = y;
            this.numerPoziomu = n;
            this.rozmiarX = x;
            this.level_layout = new Obiekt[x, y];
        }
        Obiekt getMap(int x, int y)
        {
            return level_layout[x, y];
        }
        public int revertObject(int x, int y)
    {       // Console.Write(level_layout[x, y] == null);
       // Console.Write(level_layout[x,y].test().ToString());
        return level_layout[x, y].getNumberTile();
        }
        public void placeObject(int x, int y, int n)
        {
            switch (n)
            {
                case 1:
                    level_layout[x, y] = new Wall(x, y);
                    break;
                case 2:
                    level_layout[x, y] = new Trap(1, x, y);
                    break;
                case 3:
                    level_layout[x, y] = new Exit(x, y);
                    break;
                case 4:
                    level_layout[x, y] = new PlayerMarker(x, y);
                    break;
                case 5:
                    level_layout[x, y] = new Monster(1, x, y);
                    break;
                case 6:
                    level_layout[x, y] = new Coin(1, x, y);
                    break;
                case 7:
                    level_layout[x, y] = new HPotion("heal", x, y);
                    break;
                case 8:
                    level_layout[x, y] = new IPotion("defense", x, y);
                    break;
                case 9:
                    level_layout[x, y] = new SPotion("power", x, y);
                    break;
                default:
                    if (n == 0) level_layout[x, y] = new Empty(x, y);
                    else if (n < 100) level_layout[x, y] = new Key(n, x, y);
                    else if (n < 200) level_layout[x, y] = new Door(n, x, y);
                    else if (n < 300) level_layout[x, y] = new Lever(n, x, y);
                    break;
            }

        }
    }
public class SaveManager
{
    int numberOfLevels;
    public SaveManager(int n) { numberOfLevels = n; }
    public Level[] listOfLevels;
    public void loadFiles()
    {


        listOfLevels = new Level[numberOfLevels + 1];
        for (int i = 1; i <= numberOfLevels; i++)
        {
            string s = "level" + i.ToString() + ".txt";
            string folderPath = AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                string[] textLines = File.ReadAllLines(folderPath + s);
                listOfLevels[i] = new Level(Int32.Parse(textLines[0]), Int32.Parse(textLines[1]), Int32.Parse(textLines[2]));
                for (int j = 0; j < listOfLevels[i].rozmiarY; j++)
                {
                    for (int k = 0; k < listOfLevels[i].rozmiarX; k++)
                    {
                        listOfLevels[i].placeObject(k, j, Int32.Parse(textLines[3 + k + ((j) * listOfLevels[i].rozmiarX)]));
                        // Console.WriteLine(k);
                        //Console.WriteLine(j);
                        //Console.WriteLine(listOfLevels[i].revertObject(k, j));
                    }
                }
            }

            catch
            {
                Console.WriteLine("Brak pliku " + s);
                Console.ReadLine();
                Environment.Exit(0);
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
        try
        {
            string[] textLines = File.ReadAllLines("C:\\files\\" + s);

            listOfLevels[0] = new Level(Int32.Parse(textLines[0]), Int32.Parse(textLines[1]), Int32.Parse(textLines[2]));
            for (int j = 0; j < listOfLevels[0].rozmiarY; j++)
            {
                for (int k = 0; k < listOfLevels[0].rozmiarX; k++)
                {
                   // Console.WriteLine(textLines[3 + k + ((j) * listOfLevels[0].rozmiarX)]);
                    listOfLevels[0].placeObject(k, j, Int32.Parse(textLines[3 + k + ((j) * listOfLevels[0].rozmiarX)]));
                }
            }
            return listOfLevels[0];
        }
        catch
        {
            Console.WriteLine("Brak pliku save.txt"); Console.ReadLine();
            Environment.Exit(0);
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
        return listOfLevels[n + 1];
    }
    public void saveGame(Level a, int health, int score)
    {
        File.WriteAllText("C:\\files\\save.txt", String.Empty);

        string loadOut = a.numerPoziomu.ToString() + '\n' + a.rozmiarX.ToString() + '\n' + a.rozmiarY.ToString() + '\n';
        for (int j = 0; j < a.rozmiarY; j++)
        {
            for (int k = 0; k < a.rozmiarX; k++)
            {
                loadOut = loadOut + a.revertObject(k, j).ToString() + '\n';
            }
        }
        File.WriteAllText("C:\\files\\save.txt", loadOut);

        File.WriteAllText("C:\\files\\player.txt", String.Empty);
        File.WriteAllText("C:\\files\\player.txt", health.ToString() + '\n' + score.ToString());
    }

    public int loadPlayerHealth()
    {
        string s = "player.txt";
        try
        {
            string[] textLines = File.ReadAllLines("C:\\files\\" + s);
            return Int32.Parse(textLines[0]);
        }
        catch
        {
            Console.WriteLine("Brak pliku player.txt"); Console.ReadLine();
            Environment.Exit(0);
        }
        return 0;

    }
    public int loadPlayerScore()
    {
        string s = "player.txt";
        try
        {

            string[] textLines = File.ReadAllLines("C:\\files\\" + s);
            return Int32.Parse(textLines[1]);
        }
        catch
        {
            Console.WriteLine("Brak pliku player.txt"); Console.ReadLine();
            Environment.Exit(0);
        }
        return 0;
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
        for (int i = 0; i < map.rozmiarY; i++)
        {
            for (int j = 0; j < map.rozmiarX; j++)
            {
                //Console.WriteLine(j.ToString() + " " + i.ToString());
                if (map.revertObject(j, i) == 5) numberOfMonsters++;
               // Console.WriteLine(j.ToString() + " " + i.ToString());
            }
        }
        if (numberOfMonsters > 0)
        {
            listOfMonsters = new C[numberOfMonsters];
            int g = 0;
            for (int i = 0; i < map.rozmiarY; i++)
            {
                for (int j = 0; j < map.rozmiarX; j++)
                {
                    if (map.revertObject(j, i) == 5)
                    {
                        listOfMonsters[g] = new C(j, i);
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
               
                switch (map.revertObject(j, i))
                {
                    case 0:
                        Console.Write(".. ");
                        break;
                    case 1:
                        Console.Write("[] ");
                        break;
                    case 3:
                        Console.Write("-> ");
                        break;
                    case 4:
                        Console.Write("() ");
                        break;
                    case 2:
                        Console.Write(("XX "));
                        break;
                    case 5:
                        Console.Write(("== "));
                        break;
                    case 6:
                        Console.Write(("$$ "));
                        break;
                    case 7:
                        Console.Write(("Z+ "));
                        break;
                    case 8:
                        Console.Write(("N+ "));
                        break;
                    case 9:
                        Console.Write(("P+ "));
                        break;
                    default:
                        if (map.revertObject(j, i) > 199) Console.Write(("\\\\ "));
                        else if (map.revertObject(j, i) > 99) Console.Write((map.revertObject(j, i)-100)+ " ");
                        else Console.Write(("k- "));
                        break;
                }

            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
    public C searchPlayer()
    {
        for (int i = 0; i < map.rozmiarY; i++)
        {
            for (int j = 0; j < map.rozmiarX; j++)
            {
                if (map.revertObject(j, i) == 4) return new C(j, i);
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
        if (map.revertObject(expectedPosition.x, expectedPosition.y) > 99 && map.revertObject(expectedPosition.x, expectedPosition.y) < 200) return player.doorCheck(map.revertObject(expectedPosition.x, expectedPosition.y), 0);
        if (map.revertObject(expectedPosition.x, expectedPosition.y) > 200)
        {
            for (int i = 0; i < map.rozmiarY; i++)
                for (int j = 0; j < map.rozmiarX; j++)
                    if (map.revertObject(j, i) == map.revertObject(expectedPosition.x, expectedPosition.y) - 200) map.placeObject(j, i, 0);
            return false;
        }
        if (map.revertObject(expectedPosition.x, expectedPosition.y) != 1) return true;
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
        if (map.revertObject(expectedPosition.x, expectedPosition.y) == 0) return true;
        else return false;
    }
    public bool isMonsterAttack(C Mposition, C Pposition)
    {
        int differencex = Mposition.x - Pposition.x;
        int differeney = Mposition.y - Pposition.y;
        if (differencex == -1 && differeney == 0) return true;
        if (differencex == 1 && differeney == 0) return true;
        if (differencex == 0 && differeney == 1) return true;
        if (differencex == 0 && differeney == -1) return true;
        return false;
    }
    public void monsterMove(bool isProtected)
    {
        for (int i = 0; i < numberOfMonsters; i++)
        {
            if (listOfMonsters[i].x == -1) { }
            else if (isMonsterAttack(listOfMonsters[i], player.getPosition()) == true && isProtected == false) player.damage();
            else
            {

                bool legality = false;
                int bugFix = 100;
                C expectedTile = new C(-1, -1);
                while (legality == false && bugFix > 0)
                {
                    bugFix--;
                    Random rnd = new Random();
                    int direction = rnd.Next(1, 5);
                    switch (direction)
                    {
                        case 1: legality = isMoveLegalMonsterEdition(listOfMonsters[i], 'g'); expectedTile = new C(listOfMonsters[i].x, listOfMonsters[i].y - 1); break;
                        case 2: legality = isMoveLegalMonsterEdition(listOfMonsters[i], 'd'); expectedTile = new C(listOfMonsters[i].x, listOfMonsters[i].y + 1); break;
                        case 3: legality = isMoveLegalMonsterEdition(listOfMonsters[i], 'p'); expectedTile = new C(listOfMonsters[i].x + 1, listOfMonsters[i].y); break;
                        case 4: legality = isMoveLegalMonsterEdition(listOfMonsters[i], 'l'); expectedTile = new C(listOfMonsters[i].x - 1, listOfMonsters[i].y); break;
                    }
                }
                map.placeObject(expectedTile.x, expectedTile.y, 5);
                map.placeObject(listOfMonsters[i].x, listOfMonsters[i].y, 0);
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
                    movedTile = map.revertObject(expectedPosition.x, expectedPosition.y);
                    break;
                }
            case 'g':
                {
                    expectedPosition = new C(position.x, position.y - 1);
                    movedTile = map.revertObject(expectedPosition.x, expectedPosition.y);
                    break;
                }
            case 'l':
                {
                    expectedPosition = new C(position.x - 1, position.y);
                    movedTile = map.revertObject(expectedPosition.x, expectedPosition.y);
                    break;
                }
            case 'p':
                {
                    expectedPosition = new C(position.x + 1, position.y);
                    movedTile = map.revertObject(expectedPosition.x, expectedPosition.y);
                    break;
                }
        }
        map.placeObject(expectedPosition.x, expectedPosition.y, 4);
        map.placeObject(position.x, position.y, 0);
    }
    public int moveTile()
    {
        return movedTile;
    }
    public void killMonster(C position)
    {

        for (int i = 0; i < numberOfMonsters; i++)
        {

            if (position.x == listOfMonsters[i].x && position.y == listOfMonsters[i].y)
            {

                listOfMonsters[i] = new C(-1, -1);
            }
        }
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
    public Player(int health, int score)
    {
        this.health = health;
        this.score = score;
    }
    public int getHealth() { return health; }
    public void heal() { this.health = 5; }
    public int getScore() { return score; }
    public int getKeys() { return keyHolder; }
    public C getPosition() { return position; }
    public void protectionPotionOn() { protection = true; }
    public void powerPotionOn() { power = true; }
    public void PotionOff() { protection = false; power = false; }
    public bool isProtected() { return protection; }
    public bool isPowerful() { return power; }
    public void printStatus() { if (protection == true) { Console.WriteLine("Gracz jest nieśmiertelny!"); } if (power == true) Console.WriteLine("Gracz jest potężny!"); }
    public bool isAlive()
    {
        if (this.health > 0) return true;
        else return false;
    }
    public void printStats()
    {
        Console.WriteLine("Zdrowie: " + this.health + " Wynik: " + this.score);
        Console.WriteLine("Posiadane klucze:" + this.keyHolder);
    }
    public void damage() { health--; }
    public void treasure() { score++; }

    public void collectKey(int a)
    {
        keyHolder *= 100;
        keyHolder += a;
    }
    public bool doorCheck(int a, int g)
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

    public Controller(SaveManager manager, MapEngine engine, Player player)
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
            Console.WriteLine("\"Nowa\" lub \"Wczytaj\"");
            Console.WriteLine("Ruch: 'w' 'a' 's' 'd'");
            Console.WriteLine("\"Zapisz\" gre");
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
        int guard = 0;
        int potionTimer = 0;
        while (player.isAlive())
        {
            if (Console.KeyAvailable)
            {
               // ConsoleKeyInfo KKey = Console.ReadKey(true);
                if (potionTimer > 0) potionTimer--;
                if (potionTimer == 0)
                {
                    player.PotionOff();
                }
               
                ConsoleKeyInfo input = Console.ReadKey();
                Console.WriteLine(input.Key.ToString());
                switch (input.Key.ToString())
                {
                    case "P": manager.saveGame(engine.getMap(), player.getHealth(), player.getScore()); break;
                    case "W":
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
                    case "S":
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
                    case "A":
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
                    case "D":
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
                switch (engine.moveTile())
                {
                    case 0: break;

                    case 3:
                    
                        if (manager.isLastLevel(engine.getMap().numerPoziomu) == false && guard == 1)
                        {
                            engine.newLevel(manager.nextLevel(engine.getMap().numerPoziomu));
                            player.position = engine.searchPlayer();
                            player.keyHolder = 0;
                        }
                        else if (guard == 1)
                        {
                            Console.Clear();
                            Console.WriteLine("Wielki Sukces! Wynik końcowy: " + player.getScore().ToString());
                           
                            Console.ReadKey();
                            Environment.Exit(0);
                        }
                        break;
                    case 5:
                    
                        if (guard == 1 && player.isPowerful() == false)
                        {
                            player.damage();
                            player.damage();
                            player.damage();
                            player.damage();
                            player.damage();
                        }
                        else if (guard == 1)
                        {
                           // Console.ReadLine();
                            engine.killMonster(player.position);
                        }
                        break;
                    case 2:
                        if (guard == 1 && player.isProtected() == false) player.damage();
                            break;
                    case 6:
                    
                        if (guard == 1) player.treasure();
                            break;
                    case 7:
                        if (guard == 1) player.heal();
                            break;
                    case 8:
                    
                        if (guard == 1)
                        {
                            player.protectionPotionOn();
                            potionTimer += 25;
                        }
                            break;
                    case 9:
                    
                        if (guard == 1)
                        {
                            player.powerPotionOn();
                            potionTimer += 25;
                        }
                        break;
                    default:
                    if (engine.moveTile() < 100 && engine.moveTile() > 9)
                    {
                        if (guard == 1)
                        {
                            player.collectKey(engine.moveTile());
                        }
                    }
                        break;
                }
                engine.monsterMove(player.isProtected());
                Console.Clear();
                engine.printMap();
                player.printStats();
                player.printStatus();
            }
            
            if (player.isAlive() == false)
            {
                Console.Clear();
                Console.WriteLine("Koniec gry! Wynik końcowy: " + player.getScore().ToString());
            }
        }
    }
}
class Man
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        SaveManager manager = new SaveManager(5);
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
    
    