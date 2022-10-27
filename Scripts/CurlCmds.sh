#!/usr/bin/env bash   

#Get all jokes
echo "GET ALL JOKES"
curl https://localhost:7149/joke

#Get one random joke
echo
echo
echo "GET ONE RANDOM JOKE"
curl https://localhost:7149/joke/random

#Get one joke selected by joke ID
echo
echo
echo "GET ONE JOKE SELECTED BY JOKE ID No 3"
curl https://localhost:7149/joke/3

