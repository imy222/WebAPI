#!/usr/bin/env bash   

LBLUE="\033[94m"
ENDCOLOR="\033[0m"

#Get all jokes
echo -e "\n\n${LBLUE}GET ALL JOKES ${ENDCOLOR}"
curl https://localhost:7149/joke

#Get one joke selected by joke ID
echo -e "\n\n${LBLUE}GET ONE JOKE SELECTED BY JOKE ID No 3 ${ENDCOLOR}"
curl https://localhost:7149/joke/3


#POST new joke
echo -e "\n\n${LBLUE}POST ONE NEW JOKE ${ENDCOLOR}"
curl -X 'POST' \
  'https://localhost:7149/joke' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
        "Id" : 6,
        "Question" : "What can Pikachu play with a baby?",
        "Punchline" : "Pika-Boo!",
        "CategoryId" : 1
}'
