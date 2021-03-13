openssl req -new -x509 -newkey rsa:2048 -sha256 -nodes -keyout server.key -days 3560 -out server.crt -config certificate.cnf
