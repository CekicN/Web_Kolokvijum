export class papir
{
    constructor(id, naziv)
    {
        this.id = id;
        this.naziv = naziv;
    }

    crtaj(host)
    {
        var div = document.createElement("div");
        div.className = "slika"
        host.appendChild(div);

        var h = document.createElement("h4");
        h.innerHTML = this.naziv;
        div.appendChild(h);

        h = document.createElement("h4");
        h.innerHTML = "slika";
        div.appendChild(h);

        h = document.createElement("button");
        h.innerHTML = "kupi";
        h.onclick = (ev) => klik(ev);
        div.appendChild(h);
    }
}

function klik(ev)
{
    var kliknut = ev.srcElement;

    var div = document.querySelector(".rezultat");

    div.removeChild(kliknut.closest("div"));

    var s = document.querySelector(".ram")

    var id
    var o = s.querySelector("option:checked");
    if(o != null){
        id = o.value
    }
    fetch(`http://localhost:5183/Fotografija/Kupi/${this.id}/${id}`,{method: "DELETE"}).then(p => 
    {
        p.json().then(q => 
            {
                console.log(q);
            })
    })
}