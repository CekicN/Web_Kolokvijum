import { Silos } from "./Silos.js";

const host = document.body;

var container = crtajDiv(host, "container");
var levi = crtajDiv(container, "levi");
var desni = crtajDiv(container, "desni");
var h1 = document.createElement("h1");
levi.appendChild(h1);

var divSilosi = crtajDiv(levi, "silosi");
var silosi = []
fetch("silosi.json").then(p => {
    p.json().then(q => {
        q.forEach(fa => {
            h1.innerHTML = fa.naziv;
            
            fa.silos.forEach(s => {
                var silo = new Silos(s.naziv, s.kolicina);
                silosi.push(silo);
            })
            crtajDodavanje(desni, silosi);  
            silosi.forEach(s => s.crtaj(divSilosi));
        });
        
    })
})

function crtajDiv(host, klasa)
{
    var div = document.createElement("div");
    div.className = klasa;
    host.appendChild(div);

    return div;
}
function crtajDodavanje(host, lista)
{
    var dodaj = crtajDiv(host, "dodaj");
    var red = crtajDiv(dodaj,"red");

    var labela = document.createElement("label");
    labela.innerHTML = "Silos:"
    red.appendChild(labela);

    var select = document.createElement("select");
    select.className = "selectSipaj"
    red.appendChild(select);
    lista.forEach(f => {
        var op = document.createElement("option")
        op.innerHTML = f.naziv
        select.appendChild(op);
    })
    red = crtajDiv(dodaj, "red");
    labela = document.createElement("label");
    labela.innerHTML = "Kolicina:"
    red.appendChild(labela);

    select = document.createElement("input");
    select.value = 0;
    red.appendChild(select);
    
    var btn = document.createElement("button")
    btn.innerHTML = "Sipaj u silos"
    btn.className = "sipaj";
    btn.onclick = (ev) => sipaj();
    dodaj.appendChild(btn);
}

function sipaj()
{
    var kolicina = document.querySelector("input").value;
    if(parseInt(kolicina) > 2000)
    {
        alert("Ne mogu da primim toliku kolicinu");
    }
    else
    {
        var silos = desni.querySelector(".selectSipaj").querySelector("option:checked").innerHTML;
        var naziv = divSilosi.querySelectorAll(".naziv");
        var sil;
        naziv.forEach(p => {
            if(p.innerHTML == silos)
            {
                sil = p.closest("div");
            }
        })
        var koli = sil.querySelector(".kolicina");
        koli.innerHTML = `${kolicina}t/2000t`;
        var kol = sil.querySelector(".kol")
        kol.style.height = (parseInt(kolicina)/2000)*300+"px";
    }
}