FROM debian
ADD server /root
EXPOSE 8080
CMD /root/blockgame_server.x86_64
