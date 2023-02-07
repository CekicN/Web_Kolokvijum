import { Grad } from "./grad.js";
import { podaci } from "./podaci.js";

const host = document.body;

var listagradova = []

var map = new Map();
fetch("grad.json").then(p => {
    p.json().then(q => {
        q.forEach(grad => {
            var g = new Grad(grad.id, grad.naziv, grad.x,grad.y);
            listagradova.push(g);
        });
        console.log(listagradova);
        fetch("podaci.json").then(p => {
            p.json().then(q => 
            {
                q.forEach((pod, i) => {
                        if(pod.idGrada == listagradova[i].id)
                        {
                            var lista = [];
                            pod.podaci.forEach(poda => {
                                var podatak = new podaci(poda.mesec, poda.temperatura, poda.padavine, poda.suncaniDani);
                                lista.push(podatak);
                            })
                            map.set(listagradova[i].naziv,lista);
                        }
                })
                console.log(map);
                listagradova.forEach(grad => {
                    grad.crtaj(host, map.get(grad.naziv));
                })
                
            })
        })
    })
})