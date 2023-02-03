import { Student } from "./Student.js";

export class Fakultet
{
    constructor(listaPredmeta, listaRokova)
    {
        this.listaPredmeta = listaPredmeta;
        this.listaRokova = listaRokova;
    }
    crtaj(host)
    {
        this.kont = document.createElement("div");
        this.kont.className = "GlavniKontejner";
        host.appendChild(this.kont);

        let kontForma = document.createElement("div");
        kontForma.className = "Forma";
        this.kont.appendChild(kontForma);

        

        this.crtajFormu(kontForma);
        this.crtajPrikaz(this.kont);

    }
    crtajPrikaz(host)
    {
        let kontPrikaz = document.createElement("div");
        kontPrikaz.className = "Prikaz";
        host.appendChild(kontPrikaz);

        var tabela = document.createElement("table");
        tabela.className = "tabela";
        kontPrikaz.appendChild(tabela);

        var tabelahead = document.createElement("thead");
        tabela.appendChild(tabelahead);

        var tr = document.createElement("tr");
        tabelahead.appendChild(tr);

        var tabelaBody = document.createElement("tbody");
        tabelaBody.className = "TabelaPodaci";
        tabela.appendChild(tabelaBody);

        let th;
        var zag = ["Indeks", "Ime", "Prezime", "Predmet", "Rok", "Ocena"];
        zag.forEach(el => {
            th = document.createElement("th");
            th.innerHTML = el;
            tr.appendChild(th);
        })

    }

    crtajRed(host)
    {
        let red = document.createElement("div");
        red.className = "red";
        host.appendChild(red);
        return red;
    }
    //Crtanje predmeta
    crtajFormu(host)
    {
        let red = this.crtajRed(host);
        let l = document.createElement("label");
        l.innerHTML = "Ispit";
        red.appendChild(l);

        let s = document.createElement("select");
        red.appendChild(s);
        let op;
        this.listaPredmeta.forEach(opcija => {
            op = document.createElement("option");
            op.innerHTML = opcija.naziv;
            op.value = opcija.id;
            s.appendChild(op);
        });

        //Crtanje listaRokova
        red = this.crtajRed(host);
        l = document.createElement("label");
        l.innerHTML = "Rok";
        red.appendChild(l);

        let cbox = document.createElement("div");
        cbox.className = "cbox";
        red.appendChild(cbox);

        let levi = document.createElement("div");
        levi.className = "levi";
        cbox.appendChild(levi);

        let desni = document.createElement("div");
        desni.className = "desni";
        cbox.appendChild(desni);

        let cb;
        this.listaRokova.forEach((rok,i) => {
            cb = document.createElement("input");
            cb.type = "checkbox";
            cb.value = rok.id;
            l = document.createElement("label");
            l.innerHTML = rok.naziv;
            if(i%2 === 0)
            {
                red = this.crtajRed(levi);
                red.appendChild(cb);
                red.appendChild(l);
            }
            else
            {
                red = this.crtajRed(desni);
                red.appendChild(cb);
                red.appendChild(l);
            }
            
        })
        red = this.crtajRed(host);
        let btnNadji = document.createElement("button");
        btnNadji.onclick = (ev)=>this.nadjiStudente();
        btnNadji.innerHTML = "Nadji";
        red.appendChild(btnNadji);

        red = this.crtajRed(host);
        l = document.createElement("label");
        l.innerHTML = "Indeks";
        red.appendChild(l);
        red = this.crtajRed(host);
        var indeks = document.createElement("input");
        indeks.type = "number";
        indeks.className = "BrojIndeksa";
        red.appendChild(indeks);

        red = this.crtajRed(host);
        l = document.createElement("label");
        l.innerHTML = "Ocena";
        red.appendChild(l);
        red = this.crtajRed(host);
        var ocena = document.createElement("input");
        ocena.type = "number";
        ocena.className = "Ocena";
        red.appendChild(ocena);

        red = this.crtajRed(host);
        btnNadji = document.createElement("button");
        btnNadji.onclick = (ev)=>this.upisiStudente(indeks.value, ocena.value);
        btnNadji.innerHTML = "Upisi";
        red.appendChild(btnNadji);
    }

    nadjiStudente()
    {
        let predmet = this.kont.querySelector("select");
        var ispitId = predmet.options[predmet.selectedIndex].value;
        
        let rokovi = this.kont.querySelectorAll("input[type='checkbox']:checked");
        if(rokovi === null)
        {
            alert("Nisu izabrani rokovi");
            return;
        }
        let nizRokova = ""; 
        let RokoviIDs = [];
        rokovi.forEach(rok => {
           nizRokova = nizRokova.concat(rok.value,"a");
           RokoviIDs.push(rok.value);
        });
        console.log(nizRokova);

        //this.ucitajStudente(ispitId, nizRokova);
        this.ucitajStudenteFromBody(ispitId, RokoviIDs);
    }

    ucitajStudente(ispitId, nizRokova)
    {

        fetch(`https://localhost:5001/Student/StudentiPretraga/${nizRokova}/${ispitId}`,
        {
            method:"GET"
        }).then(x => {
            if(x.ok)
            {
                var teloTabele = this.obrisiPrethodniSadrzaj();
                x.json().then(studenti => {
                    studenti.forEach(student => {
                        let p = new Student(student.index, student.ime,student.prezime, student.predmet, student.rok,student.ocena);
                        p.crtaj(teloTabele);
                    });
                    
                    
                })
            }
           
        })
    }


    ucitajStudenteFromBody(ispitId, RokoviIDs)
    {

        fetch(`https://localhost:5001/Student/StudentiPretragaFromBody/${ispitId}`,
        {
            method:"PUT",
            headers:{
                "Content-Type":"application/json"
            },
            body:JSON.stringify(RokoviIDs)
        }).then(x => {
            if(x.ok)
            {
                var teloTabele = this.obrisiPrethodniSadrzaj();
                x.json().then(studenti => {
                    studenti.forEach(student => {
                        let p = new Student(student.index, student.ime,student.prezime, student.predmet, student.rok,student.ocena);
                        p.crtaj(teloTabele);
                    });
                    
                    
                })
            }
           
        })
    }
    upisiStudente(indeks,ocena)
    {
        if(indeks === null || indeks === undefined || indeks === "")
        {
            alert("Unesite broj ideksa!");
            return;
        }
        if(ocena === "")
        {
            alert("Unesite ocenu!")
            return
        }
        else
        {
            var oc = parseInt(ocena);
            if(oc < 5 || oc > 10)
            {
                alert("Neispravna ocena");
                return;
            }
        }
        let rokovi = this.kont.querySelectorAll("input[type='checkbox']:checked");
        if(rokovi.length != 1 || rokovi === null)
        {
            alert("Morate izabrati samo jedan rok");
            return;
        }
        let select = this.kont.querySelector("select");
        let predmetID = select.options[select.selectedIndex].value;
        fetch(`https://localhost:5001/Ispit/DodajPolozeniIspit/${indeks}/${predmetID}/${rokovi[0].value}/${ocena}`, {
            method:"POST"
        }).then(x => {
            if(x.ok)
            {
                var teloTabele = this.obrisiPrethodniSadrzaj();
                x.json().then(studenti => {
                    studenti.forEach(student => {
                        let s = new Student(student.indeks, student.ime,student.prezime, student.predmet, student.ispitniRok,student.ocena);
                        s.crtaj(teloTabele);
                    })
                })
            }
        });
    }
    obrisiPrethodniSadrzaj()
    {
        let sadrzaj = this.kont.querySelector(".TabelaPodaci");
        let roditelj = sadrzaj.parentElement;
        roditelj.removeChild(sadrzaj);

        sadrzaj = document.createElement("tbody");
        sadrzaj.className = "TabelaPodaci";
        roditelj.appendChild(sadrzaj);
        return sadrzaj;
    }

}

