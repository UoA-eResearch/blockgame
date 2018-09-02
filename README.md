# blockgame
A simple HTML5 game involving stacking blocks

## How it works

This is a 2D Unity game, using Unet for the multiplayer functionality. The WebGL version of the game is hosted on GitHub, and is served from the docs folder as per GitHub convention. The server can be run as a Docker container - the Dockerfile in this repository is used via DockerHub automated build (see https://hub.docker.com/r/uoacer/blockgame/builds/). The NeCTAR server (r.nectar.auckland.ac.nz) is running v2tec/watchtower, so will automatically update the running server container when DockerHub publishes a new build. Additionally, the NeCTAR server is running Apache2 with proxy_wstunnel to provide an SSL wrapper (wss->ws) around the websocket. This is necessary as GitHub forces HTTPS
