# One Last Time Before Edereta Devours The World

<div align="center">
  <img src="https://i.imgur.com/ZciFmPR.png" alt="By Fernando Esra" width="300">
</div>

---

_OLTBEDTW is a game of exploration and mystery. Aunia must travel through a once friendly, now strange land, to decide how to spend her last days._

---

OLTBEDTW was my final year project for my higher studies "Cross-Platform Application Development (DAM in spanish)" which I finished in 2024. The project was quite a challenge, in just 3 months I had to go through all the stages of creating a video game. From the initial study of the state of the art, deep research into the history of CRPGs, analysis of game creation engines, study of databases, design of mechanics, creation of Tilesets and sprites and assembly of the game loop. All while combining this task with a full-time job.

It was certainly a complicated challenge, but the final result was worth it.

# Gameplay mechanics:

Aunia starts the day by her home. She has a long journey ahead of her to reach Stillenss I. She must explore the ruins of her old world to find resources to survive the journey.

- Inventory system
- In-game statistics (Health, Sanity, Hunger and Fatigue)
- Environment exploration
- Decision mechanics in exploration
- World map
- Persistent statistics between games (steps, deaths, endings)
- Two languages ​​(Spanish and English)
- Two endings

<div align="center">
  <img src="https://i.imgur.com/0wq0HLX.png" alt="Starting place" width="800">
  <br>
  <p>Starting place (Aunia's home)</p>
</div>

<div align="center">
  <img src="https://i.imgur.com/fE3gFAz.png" alt="World Map" width="800">
  <br>
  <p>World Map</p>
</div>

In the final state that the project was delivered, the game worked from start to finish. All graphics were complete and persistent data worked. The only problem was the lack of content. Due to the short time available for development, the creation of the environment was prioritized over content. There are hardly any places to visit and the map is very small. But it serves to show what the project is capable of.

The map is randomly generated in each session. The description of the places, their decisions and content are in a database. If you want to add new places, you edit a .csv file and the new place will appear in the next game.

# The magic of design

The first challenge when designing the game was to conduct a study of the state of the art at a specific time in history. The game aims to emulate the way old CRPGs are played, games like [Eye of the Beholder](https://en.wikipedia.org/wiki/Eye_of_the_Beholder_(video_game)) or [Ultima I](https://es.wikipedia.org/wiki/Ultima_I:_The_First_Age_of_Darkness). To do this, a large number of sources were consulted, both open (such as [The CRPG Book Project](https://crpgbook.wordpress.com/)) and private ([A Guide to Japanese Role-Playing Games](https://www.bitmapbooks.com/products/a-guide-to-japanese-role-playing-games), [The CRPG Book](https://www.bitmapbooks.com/products/the-crpg-book-a-guide-to-computer-role-playing-games), others.). Inside the documents folder, you can consult several .pdf files (in Spanish) with the entire process in more detail.

The most important points of development in the design stage were the following:
- First, documentation and sketches (with the help of [Irene Lloret](https://linktr.ee/TuturuArt)) for the interface.
- First sketches of game mechanics

<div align="center">
  <img src="https://i.imgur.com/VBFHvNn.png" width="800">
  <br>
  <p>Irene's sketches for the interface</p>
</div>

- Choice of the color palette

<div align="center">
  <img src="https://i.imgur.com/pfi4kuP.png" width="300">
  <br>
  <p>Palette for objects and most of the interface. TILESET uses a palette with more colors</p>
</div>

- Creation of frames for the interface and icons

<div align="center">
  <img src="https://i.imgur.com/4unIptg.png="800">
  <br>
  <p>Creating icons using Aseprite</p>
</div>

- Creation of the TILESET

<div align="center">
  <img src="https://i.imgur.com/hjJ0Tv4.gif="800">
  <br>
  <p>Animated Tileset</p>
</div>

The icons and frames were designed using free or purchased designs (with a modification license) on websites such as [https://itch.io/](itchi.io). The TILEST has been designed from scratch, along with its animations.

TILESET LICENSE: The TILESET has been created by Fernando Esra and is 100% public, anyone can use it as long as they cite the author in the credits. A downloadable version can be found in the public_assets folder.

# The magic of programming

**Under Construction**
