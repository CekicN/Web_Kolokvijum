export class Grad
{
    constructor(id, naziv, x, y)
    {
        this.id = id;
        this.naziv = naziv;
        this.x = x;
        this.y = y;
    }
    crtaj(host, lista)
    {
        var container = this.crtajDiv(host, "container");

        var h1 = document.createElement("h1");
        h1.innerHTML = `Grad ${this.naziv} (${this.x} N, ${this.y} E), godina:2020`;
        container.appendChild(h1);

        var radioDiv = this.crtajDiv(container, "radioDiv");
        var lista1 = ["Temperatura", "Padavine", "Suncani dani"];
        this.crtajRadio(radioDiv, lista1);

        var btn = document.createElement("button");
        btn.innerHTML = "Prikazi";
        btn.onclick = (ev) => this.prikazi(container, lista);
        container.appendChild(btn);
    }
    crtajDiv(host, klasa)
    {
        var div = document.createElement("div");
        div.className = klasa;
        host.appendChild(div);

        return div;
    }
    crtajRadio(host, lista)
    {
        
        lista.forEach((r,i) => {
            
            var div = this.crtajDiv(host,"red");
            var radio = document.createElement("input");
            radio.type = "radio";
            radio.name = this.naziv;
            radio.checked = true;
            radio.value = i;
            div.appendChild(radio);

            var labela = document.createElement("label");
            labela.innerHTML = r;
            div.appendChild(labela)
            
        });
    }
    prikazi(host, lista)
    {   

        var d = host.querySelector(".poda");
        if(d != null)
        {
            host.removeChild(d);
        }

        var val = host.querySelector("input[type='radio']:checked").value;
        var poda = this.uzmiPodatke(lista, val);
        var podaci = this.crtajDiv(host, "poda");
        
        lista.forEach((podatak,i) => {
            var cont = this.crtajDiv(podaci, "cont");
            
            var p = document.createElement("p");
            p.className = "t"
            p.innerHTML = `${poda[i]} C`;
            cont.appendChild(p);

            p = document.createElement("p");
            p.className = "mesec"
            p.innerHTML = podatak.mesec;
            cont.appendChild(p);
            
            var pod = this.crtajDiv(cont, "pod");
            pod.onclick = (ev) => this.promeni(ev, podaci);
            var c = (parseInt(poda[i])/(poda[12]+30))*150;
            console.log(c);
            pod.style.height = c+"px";
        })
        
    }
    uzmiPodatke(lista,val)
    {
        var l = [];
        lista.forEach(p => {
            if(val == 0)
            {
                l.push(p.temperatura);
            }
            else if(val == 1)
            {
                l.push(p.padavine);
            }
            else
            {
                l.push(p.suncaniDani);
            }
        })

        l.push(Math.max.apply(null,l));
        return l;
    }
    promeni(ev, host)
    {
        var kliknut = ev.srcElement.parentElement;
        var mesec = kliknut.querySelector(".mesec");

        var d = host.querySelector(".promeni");
        if(d != null)
        {
            host.removeChild(d);
        }

       var div = this.crtajDiv(host, "promeni");

        var p = document.createElement("p");
        p.innerHTML = `Mesec: ${mesec.innerHTML}`
        div.appendChild(p);

        var inp = document.createElement("input");
        inp.value = 0;
        div.appendChild(inp);

        var btn = document.createElement("button");
        btn.innerHTML = "Sacuvaj izmene";
        btn.onclick = (e) => this.pr(kliknut);
        div.appendChild(btn);
    }
    pr(kliknut)
    {
        var input = document.querySelector(".promeni");
        var par = input.parentElement;
        var val = input.querySelector("input").value;
        var t = kliknut.querySelector(".t");
        console.log(t);
        console.log(val);
        t.innerHTML = val;

        var pod = kliknut.querySelector(".pod");
        var c = (parseInt(val)/(100))*150;
        pod.style.height = c+"px";

        par.removeChild(input);
    }
}