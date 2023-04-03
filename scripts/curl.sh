#!/usr/bin/env bash   

LBLUE="\033[94m"
ENDCOLOR="\033[0m"

#Get all jokes
echo -e "\n\n${LBLUE}GET ALL JOKES ${ENDCOLOR}"
curl https://localhost:7149/joke | jq

#Get one joke selected by joke ID
echo -e "\n\n${LBLUE}GET ONE JOKE SELECTED BY JOKE ID No 3 ${ENDCOLOR}"
curl https://localhost:7149/joke/3 |jq

#POST new joke
echo -e "\n\n${LBLUE}POST ONE NEW JOKE ${ENDCOLOR}"
curl -X 'POST' \
  'https://localhost:7149/joke' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
        "Question" : "What can Pikachu play with a baby?",
        "Punchline" : "Pika-Boo!",
}'
echo -e "\n\n${LBLUE}GET ALL JOKES AFTER POSTING ONE JOKE ${ENDCOLOR}"
curl https://localhost:7149/joke/6 | jq


#PUT to existing joke and get joke to view joke is updated
echo -e "\n\n${LBLUE}PUT ONE NEW JOKE AND THEN curl GET ALL TO CONFIRM JOKE UPDATED ${ENDCOLOR}"
curl -X 'PUT' \
  'https://localhost:7149/joke/4' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
        "Question" : "What do you call Meowth'\''s reflection?",
        "Punchline" : "TESTING",
}'
curl https://localhost:7149/joke/4 | jq



