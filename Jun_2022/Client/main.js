import { Automobil } from "./Automobil.js";
import { Marka } from "./Marka.js";

var host = document.body;
var listaMarki = [];
var listaModela = [];
var listaBoja = [];

var container = document.createElement("div");
container.className = "container";
host.appendChild(container);

var div = document.createElement("div");
div.className = "pretraga";
container.appendChild(div);

var rez = document.createElement("div");
rez.className = "rezultat";
container.appendChild(rez);
var crtajred = (h) => 
{
    var d = document.createElement("div");
    d.className = "red";
    h.appendChild(d);

    return d;
}

///DEO ZA PRETRAGU...

//SELECT za Marke
fetch("http://localhost:5270/Automobil/PreuzmiMarku")
.then(p =>{
     p.json().then(marke => {
     marke.forEach(e => {
        let m = new Marka(e.id, e.naziv);
        listaMarki.push(m);
     });
         //Crtanje marki
         var red = crtajred(div);
         var l = document.createElement("label")
         l.innerHTML = "Marka:";
         red.appendChild(l)
 
         var s = document.createElement("select")
         s.className = "sMarka";
         red.appendChild(s);

         var o = document.createElement("option");
         o.innerHTML = "selektuj marku";
         o.style.display = "none";
         s.appendChild(o);
         listaMarki.forEach(p => {
             o = document.createElement("option")
             o.innerHTML = p.naziv;
             s.appendChild(o)
         });
        
         //SELECT za Model
         var sMarka = div.querySelector(".sMarka");
         var ime
         red = crtajred(div);
         l = document.createElement("label")
         l.innerHTML = "Model:";
        red.appendChild(l)
            
        s = document.createElement("select")
        s.className = "sModel";
        red.appendChild(s);
        o = document.createElement("option");
        o.innerHTML = "selektuj model";
        o.style.display = "none";
        s.appendChild(o);

         //SELECT za Boju
         red = crtajred(div);
         var sModel = div.querySelector(".sModel");
         l = document.createElement("label")
         l.innerHTML = "Boja:";
        red.appendChild(l)
            
        s = document.createElement("select")
        s.className = "sBoja";
        red.appendChild(s);

        o = document.createElement("option");
        o.innerHTML = "selektuj boju";
        o.style.display = "none";
        s.appendChild(o);

        //Dugme Pronadji
       var Pronadji = document.createElement("button");
        Pronadji.innerHTML = "pronadji";
        div.appendChild(Pronadji);
        Pronadji.addEventListener('click', prikazi);
        //Listener -> na promenu marke menjaju se i modeli
         sMarka.addEventListener('change', () => {
            var op = sMarka.querySelector("option:checked");
            //Praznim listuModela
            listaModela.splice(0,listaModela.length);
            //Pronalazim Marku sa selektovanim imenom
            listaMarki.forEach(e => {
                if(e.naziv == op.innerHTML)
                {
                    ime = e;
                }
            })
            //Preuzimanje Modela po idju Marke
            fetch(`http://localhost:5270/Automobil/PreuzmiModel/${ime.id}`).then(
                f =>{
                    f.json().then(modeli => {
                        modeli.forEach(model => {
                            var m = new Marka(model.id, model.naziv);
                            listaModela.push(m);
                        })

                        //Brisanje option u selecet od Modela
                        if(sModel.length > 1)
                        {
                            for(let i = sModel.options.length; i > 0; i--)
                            {
                                sModel.remove(i);
                            }
                            
                        }
                        //Dodavanje option u select od Modela
                        listaModela.forEach(p => {
                            o = document.createElement("option")
                            o.innerHTML = p.naziv;
                            sModel.appendChild(o)
                         });
                    })
                });
         })  
         //SELECT za Boje
         sModel.addEventListener('change', () => {
            var m = sModel.querySelector("option:checked");
            //Praznim listuBoja
            listaBoja.splice(0,listaBoja.length);
            //Pronalazim Model sa selektovanim imenom
            var boja;
            listaModela.forEach(e => {
                if(e.naziv == m.innerHTML)
                {
                    boja = e;
                }
            })
            fetch(`http://localhost:5270/Automobil/PreuzmiBoju/${boja.id}`).then(p => {
                p.json().then(boje => {
                    boje.forEach(boja => {
                        var bo = new Marka(boja.id, boja.naziv);
                        listaBoja.push(bo);
                    })
                    //Brisanje option u selecet od Boja
                    var sBoja = div.querySelector(".sBoja");
                    
                    if(sBoja.length > 1)
                    {
                        console.log(sBoja);
                        for(let i = sBoja.options.length; i > 0; i--)
                        {
                            sBoja.remove(i);
                        }
                        
                    }
                    //Dodavanje option u select od Boja
                    
                    listaBoja.forEach(p => {
                        o = document.createElement("option")
                        o.innerHTML = p.naziv;
                        sBoja.appendChild(o);
                    });

                    
                })
            })

        })    
    })
});


//DEO SA REZULTATIMA PRETRAGE
var listaAutomobila = []
function prikazi()
{
    listaAutomobila = []
    var ma = div.querySelector(".sMarka option:checked");
    var mo = div.querySelector(".sModel option:checked");
    var bo = div.querySelector(".sBoja option:checked");

    var marka;
    listaMarki.forEach(e => {
        if(e.naziv == ma.innerHTML)
        {
            marka = e;
        }
    })
    var model;
    listaModela.forEach(e => {
        if(e.naziv == mo.innerHTML)
        {
            model = e;
        }
    })
    var boja;
    listaBoja.forEach(e => {
        if(e.naziv == bo.innerHTML)
        {
            boja = e;
        }
    })

    if(ma.innerHTML == "selektuj marku")
    {
        alert("morate selektovati barem marku");
    }
    else
    {
        var str = "";

        if(model == null)
        {
            str = `IDs=${marka.id}`;
        }
        else if(boja == null)
        {
            str = `IDs=${marka.id}&IDs=${model.id}`;
        }
        else
        {
            str = `IDs=${marka.id}&IDs=${model.id}&IDs=${boja.id}`;
        }

        fetch(`http://localhost:5270/Automobil/PronadjiAutomobil?${str}`,{method: "GET"}).then(p => 
        {
            p.json().then(automobili => {
                automobili.forEach(auto => {
                    var a = new Automobil(auto.marka, auto.model, auto.slika, auto.kolicina, auto.datum, auto.cena);
                    listaAutomobila.push(a);
                })
                var parent = container.querySelector(".rezultat");
                var child = parent.lastElementChild;
                while(child)
                {
                    parent.removeChild(child);
                    child = parent.lastElementChild;
                }
                listaAutomobila.forEach(a => a.crtaj(rez));
            })
        })
    }
}

