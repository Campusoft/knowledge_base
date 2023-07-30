# WSL2



----------------------------

I found my volume data under


You can find WSL2 volumes under a hidden network share. Open Windows Explorer, and type \\wsl$ into the location bar. Hit enter, and it should display your WSL volumes, including the ones for Docker for Windows.


```
\\wsl$\docker-desktop-data\data\docker\volumes
```

# COmmandos


List existing WSL storages

```
$ wsl --list -v
```


# Referencias


HowTo: Change Docker containers storage location with WSL2 on Windows 10
https://blog.codetitans.pl/post/howto-docker-over-wsl2-location/
