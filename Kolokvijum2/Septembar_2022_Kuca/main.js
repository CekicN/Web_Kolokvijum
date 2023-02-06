import { Kuca } from "./Kuca.js";
import { materijal } from "./Materijal.js";

const host = document.body;

var h = document.createElement("h1");
h.innerHTML = "Montazne kuce";
host.appendChild(h);

var glavniDiv = crtajDiv(host, "glavniDiv");
var pretraga = crtajDiv(glavniDiv, "pretraga");
var rezultat = crtajDiv(glavniDiv, "rezultat");

var listaMaterijala = []
var listaFasada = []
var listaStolarija = []
var listaKrovova = []
fetch("materijal.json").then(p => 
    {
        p.json().then(q => 
        {
            q.forEach(materija => {
                var m = new materijal(materija.materijal, materija.boja);
                listaMaterijala.push(m);
            });
            crtajSelect(pretraga,"Kuca:", listaMaterijala);
            fetch("fasada.json").then(p => 
                {
                    p.json().then(q => 
                    {
                        q.forEach(materija => {
                            var m = new materijal(materija.fasada, materija.boja);
                            listaFasada.push(m);
                        });
                        crtajSelect(pretraga,"Fasada:", listaFasada);
                        fetch("stolarija.json").then(p => 
                            {
                                p.json().then(q => 
                                {
                                    q.forEach(materija => {
                                        var m = new materijal(materija.stolarija, materija.boja);
                                        listaStolarija.push(m);
                                    });
                                    crtajSelect(pretraga,"Stolarija:", listaStolarija);
                                    fetch("krov.json").then(p => 
                                        {
                                            p.json().then(q => 
                                            {
                                                q.forEach(materija => {
                                                    var m = new materijal(materija.krov, materija.boja);
                                                    listaKrovova.push(m);
                                                });
                                                crtajSelect(pretraga,"Krov:", listaKrovova);
                                                //Dugme i kreiranje kuce
                                                var dugme = crtajDiv(pretraga, "dugme");
                                                var btn = document.createElement("button");
                                                btn.innerHTML = "Podesi"
                                                dugme.appendChild(btn);
                                                btn.onclick = (ev) => podesi();
                                            })
                                        })
                                })
                            })     
                    })
                })
        })
    })

function crtajDiv(host, klasa)
{
    var glavniDiv = document.createElement("div");
    glavniDiv.className = klasa;
    host.appendChild(glavniDiv);  
    return glavniDiv;     
}

function crtajSelect(host, la, lista)
{
    var red = crtajDiv(host, "red");

    var labela = document.createElement("label");
    labela.innerHTML = la;
    red.appendChild(labela);

    var select = document.createElement("select");
    red.appendChild(select);

    lista.forEach(p => {
        var op = document.createElement("option");
        op.innerHTML = p.naziv;
        op.value = p.boja;
        select.appendChild(op);
    })
}

function podesi()
{
    var select = pretraga.querySelectorAll("select");
    var trazi = []
    select.forEach(s => {
        var op = s.querySelector("option:checked");
        trazi.push(op.value);
    })

    var kuca = new Kuca(trazi[0], trazi[1],trazi[2],trazi[3]);
    kuca.crtaj(rezultat);
}