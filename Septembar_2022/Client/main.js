import { podrucje } from "./podrucje.js";
import { biljka } from "./Biljka.js";
var host = document.body;

var h = document.createElement("h1");
h.innerHTML = "Lekovito bilje"
host.appendChild(h);

var glavniDiv = document.createElement("div");
glavniDiv.className = "glavniDiv"
host.appendChild(glavniDiv);

var pretraga = document.createElement("div");
pretraga.className = "pretraga"
glavniDiv.appendChild(pretraga);



var rezultat = document.createElement("div");
rezultat.className = "rezultat"
glavniDiv.appendChild(rezultat);

var listaPodrucja = []
var listaCveca = []
var listaListova = []
var listaStabala = []

fetch("https://localhost:7086/Controller/VratiPodrucje").then(p => 
{
    p.json().then(q => 
    {
        q.forEach(po => {
            var pod = new podrucje(po.id, po.naziv);
            listaPodrucja.push(pod);
        });
        crtaj(pretraga, "Podrucje:", listaPodrucja);
        fetch("https://localhost:7086/Controller/VratiCvet").then(p => 
        {
            p.json().then(q => 
            {
                q.forEach(po => {
                    var pod = new podrucje(po.id, po.izgled);
                    listaCveca.push(pod);
                });
                crtaj(pretraga, "Cvet:", listaCveca);
                fetch("https://localhost:7086/Controller/VratiList").then(p => 
                {
                    p.json().then(q => 
                    {
                        q.forEach(po => {
                            var pod = new podrucje(po.id, po.izgled);
                            listaListova.push(pod);
                        });
                        crtaj(pretraga, "List:", listaListova);
                        fetch("https://localhost:7086/Controller/VratiStablo").then(p => 
                        {
                            p.json().then(q => 
                            {
                                q.forEach(po => {
                                    var pod = new podrucje(po.id, po.izgled);
                                    listaStabala.push(pod);
                                });
                                crtaj(pretraga, "Stablo:", listaStabala);
                                var dugme = document.createElement("div");
                                dugme.className = "dugme"
                                pretraga.appendChild(dugme);

                                var btn = document.createElement("button");
                                btn.innerHTML = "Pretrazi"
                                btn.onclick = (e) => pretrazi();
                                dugme.appendChild(btn);
                            })
                        })
                    })
                })
            })
        })
    })
})

var crtajRed = (host) =>
{
    var div = document.createElement("div");
    div.className = "red";
    host.appendChild(div);

    return div;
}
function crtaj(host, l, lista)
{
    var red = crtajRed(host);
    var labela = document.createElement("label");
    labela.innerHTML = l;
    red.appendChild(labela);

    var select = document.createElement("select");
    select.className = l;
    red.appendChild(select);
    
    lista.forEach(p => {
        var o = document.createElement("option");
        o.value = p.id;
        o.innerHTML = p.naziv;
        select.appendChild(o);
    })
}

function pretrazi()
{
    var IDs = []
    var select = pretraga.querySelectorAll("select");
    
    select.forEach(p => {
        var o = p.querySelector("option:checked");
        IDs.push(o.value);
    })

    PreuzmiBilje(IDs);
}

function PreuzmiBilje(id)
{
    var biljke = [];
    fetch(`https://localhost:7086/Controller/PreuzmiBiljku/${id[0]}/${id[1]}/${id[2]}/${id[3]}`, {method:"POST"}).then(p => 
    {
        p.json().then(q => 
        {
            q.forEach(bi => {
                var b = new biljka(bi.id, bi.kolicina, bi.naziv);
                biljke.push(b);
            })
            var child = rezultat.lastElementChild;
            while(child)
            {
                rezultat.removeChild(child);
                child = rezultat.lastElementChild;
            }
            biljke.forEach(b => {
                b.crtaj(rezultat);
            })
        })
    })
}