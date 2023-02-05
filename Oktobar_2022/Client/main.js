import { Dim } from "./Dim.js";
import { papir } from "./Papir.js";
var host = document.body;

var h = document.createElement("h1");
h.innerHTML = "Superfoto - 0 din";
host.appendChild(h);

var glavniDiv = document.createElement("div");
glavniDiv.className = "glavniDiv";
host.appendChild(glavniDiv);

var pretraga = document.createElement("div");
pretraga.className = "pretraga";
glavniDiv.appendChild(pretraga);

var rezultat = document.createElement("div");
rezultat.className = "rezultat";
glavniDiv.appendChild(rezultat);

var crtajred = (h) => 
{
    var d = document.createElement("div");
    d.className = "red";
    h.appendChild(d);

    return d;
}

var listaDimenzija = []
var listaPapira = []
var listaRamova = []
fetch("http://localhost:5183/Fotografija/UzmiDimenziju").then(p => 
    p.json().then(q => {
        q.forEach(e => {
            var d = new Dim(e.visina, e.sirina, e.id);
            listaDimenzija.push(d);
        })
        var red;
        var l;
        var s;
        var o;
        red = crtajred(pretraga)
        l = document.createElement("label")
        l.innerHTML = "Dimenzija:"
        red.appendChild(l)

        s = document.createElement("select")
        red.appendChild(s)

        red = crtajred(pretraga)
        l = document.createElement("label")
        l.innerHTML = "Papir:"
        red.appendChild(l)

        var sa = document.createElement("select")
        red.appendChild(sa)

        red = crtajred(pretraga)
        l = document.createElement("label")
        l.innerHTML = "Ram:"
        red.appendChild(l)

        var sad = document.createElement("select")
        sad.className = "ram";
        red.appendChild(sad)
        
        var btn = document.createElement("button");
        btn.innerHTML = "pretrazi";
        pretraga.appendChild(btn);
        //Crtanje Slika
        btn.addEventListener('click', () => {
            var lista = nadjiSliku();
            ucitajSlike(lista);
        });

        listaDimenzija.forEach(d => {
            o = document.createElement("option")
            o.innerHTML = `${d.visina}x${d.sirina}`
            o.value = d.id;
            s.appendChild(o)
        })

        s.addEventListener('change', () => {
            var di = s.querySelector("option:checked").value;
            listaRamova = [];
            var child = sad.lastElementChild;
            while(child)
            {
                sad.removeChild(child);
                child = sad.lastElementChild;
            }
            fetch(`http://localhost:5183/Fotografija/UzmiRamPoVelicini/${di}`).then(p => 
            {
                p.json().then(q => {
                    q.forEach(ram => {
                        var ra = new papir(ram.id, ram.materijal);
                        listaRamova.push(ra);
                    })
                    
                    listaRamova.forEach(z => {
                        o = document.createElement("option")
                        o.innerHTML = z.naziv;
                        o.value = z.id;
                        sad.appendChild(o)
                    })
                })
            })

        });

        fetch("http://localhost:5183/Fotografija/UzmiPapir").then(p => {
            p.json().then(q => {
                q.forEach(papi => {
                    var pa = new papir(papi.id, papi.naziv);
                    listaPapira.push(pa);
                })
                listaPapira.forEach(d => {
                    o = document.createElement("option")
                    o.innerHTML = d.naziv;
                    o.value = d.id;
                    sa.appendChild(o)
                })

            })
        })

    }));
      
function nadjiSliku()
{
    var listazaSliku = [];

    var s = document.querySelectorAll("select");
    
    s.forEach(p => {
        var o = p.querySelector("option:checked");
        if(o)
            listazaSliku.push(o.value);
    })
    return listazaSliku;
}

function ucitajSlike(lista)
{
    var listaSlika = []
    var l = (lista.length == 0)?"" : "?";
    lista.forEach(q => {
        l.concat("IDs=");
        l.concat(q);
    })
    fetch("http://localhost:5183/Fotografija/UzmiFotografiju"+ l).then(p => {
        p.json().then(q => {
            q.forEach(s => {
                var slika  = new papir(s.id,s.naziv);
                listaSlika.push(slika);
            })
            var child = rezultat.lastElementChild;
            while(child)
            {
                rezultat.removeChild(child);
                child = rezultat.lastElementChild;
            }
            listaSlika.forEach( slika => 
            {
                slika.crtaj(rezultat);
            })
        })
    })
   

}