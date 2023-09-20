# Line Interest Management

## install

1\) Import Mirror Networking in your project

<figure><img src=".gitbook/assets/image (1).png" alt=""><figcaption></figcaption></figure>

2\) just install archive of this repo and move scripts in your Unity project!

## how to use

* Set **LineInterestManagement** component on NetworkManager object
* Add **NetworkLine** component in player prefab
* Add some child gameobjects around 3D model in player prefab
* Set this childs in "LinePoints" of NetworkLine component
* Play!

## What this?

**Line Interest Management** - this is a custom script inherited from InterestManagement!

Its essence is that players release LineCast into other players. If there are no obstacles between a player and another player, then the players can see each other, but if there is a wall between them, for example, then LineCast will rest against this wall and, accordingly, the server will stop transmitting data about each other to these players

<figure><img src=".gitbook/assets/image (2).png" alt=""><figcaption></figcaption></figure>

<figure><img src=".gitbook/assets/image (3).png" alt=""><figcaption></figcaption></figure>

on paper, this may sound good, but in fact one line is not enough, because if there is even a small obstacle between the players, it will already interfere with the game!

Therefore, LineInterestManagement allows you to store a whole array of points from which the checking lines will originate. It is most desirable to arrange them like this:

<figure><img src=".gitbook/assets/image (4).png" alt=""><figcaption></figcaption></figure>

<figure><img src=".gitbook/assets/image (5).png" alt=""><figcaption></figcaption></figure>

Thus, the lines will have time to detect enemies even if you are about to run out of the zo wall

<figure><img src=".gitbook/assets/image (6).png" alt=""><figcaption></figcaption></figure>

## Why is that?

This can be very useful in competitive games like CS GO, in which you need to eliminate the possibility of cheating. This method will help protect your game from a **"WallHack"** type of cheat

Also, this method allows at least a little, but to reduce the pressure on the throughput

<video src='LineInterestManagement
/Video Presentation/Video Example.mp4' width=180/>