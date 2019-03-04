import { Component, OnInit, Input, EventEmitter, Output } from "@angular/core";
import ArticleModel from "../../models/article-model";
import { Store } from "src/app/shared/store/store";
import * as $ from "jquery";
import { SGUtil } from "src/app/shared/utils/siegrain.utils";
@Component({
  selector: "app-header",
  templateUrl: "./header.component.html",
  styleUrls: ["./header.component.scss"]
})
export class HeaderComponent implements OnInit {
  private _model: ArticleModel = new ArticleModel();

  @Input() isEditing: boolean = false;
  // 给 write-article 页面用的
  @Output() headerUpdated = new EventEmitter<ArticleModel>();

  constructor(public store: Store, public util: SGUtil) {}

  ngOnInit() {}

  get model() {
    return this._model;
  }

  @Input() set model(val) {
    this._model = val;
    this.loadCover();
  }

  loadCover() {
    if (this.store.renderFromServer) return;
    let src = this.model.cover;
    let image = new Image();
    image.onload = function() {
      $(`.header-bg`).css("background-image", "url('" + image.src + "')");
    };
    image.src = src;
  }

  onTitleChanged(val: string) {
    this.model.title = val;
    this.headerUpdated.emit(this.model);
  }
  onDigestChanged(val: string) {
    this.model.digest = val;
    this.headerUpdated.emit(this.model);
  }
}
